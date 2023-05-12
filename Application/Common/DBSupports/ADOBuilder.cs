using Application.Common.Interfaces;
using Domain.Entities.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DBSupports
{
    public class ADOBuilder
    {
        IApplicationDbContext _applicationDbContext;
        Dictionary<string, string> _columns;
        string _tableName;
        ApplicationUser _currentUser;
        SqlConnection _con;
        SqlCommand _sql;
        IConfiguration _configuration;
        public ADOBuilder(IApplicationDbContext applicationDbContext, string tableName, ApplicationUser currentUser, IConfiguration configuration)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _tableName = tableName;
            _currentUser = currentUser;
            var tab = new SqlParameter("tab", _tableName);
            var cols = _applicationDbContext.SPColumnTypes.FromSqlRaw("SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@tab", tab);
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            _columns = cols.ToDictionary(x => x.COLUMN_NAME, y => y.DATA_TYPE);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
            _con = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _sql = new SqlCommand("", _con);
        }
        public void AddParam(String paramName, String paramVal)
        {
            SqlParameter par;
            if (_columns.ContainsKey(paramName))
            {
                var paramType = GetDataType(paramName);
                paramName = "@" + paramName;
                switch (paramType.ToString())
                {
                    case "Int":
                        par = new SqlParameter(paramName, paramType);
                        if ((paramVal == "") || (paramVal == "--NULL--"))
                        {
                            par.IsNullable = true;
                            par.Value = DBNull.Value;// null;
                        }
                        else
                        {
                            par.Value = Int32.Parse(paramVal);
                        }
                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                    case "BigInt":
                        par = new SqlParameter(paramName, paramType);
                        if ((paramVal == "") || (paramVal == "--NULL--"))
                        {
                            par.IsNullable = true;
                            par.Value = DBNull.Value;// null;
                        }
                        else
                        {
                            par.Value = Int64.Parse(paramVal);
                        }
                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                    case "Decimal":
                        if (paramVal == "") paramVal = "0";
                        par = new SqlParameter(paramName, paramType);
                        par.Value = Decimal.Parse(paramVal);
                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                    case "Time":
                        if (paramVal == "")
                        {
                            paramVal = "00:00";
                        }
                        //else
                        //{
                        //    // Convert from Tenant's timezone to UTC
                        //    var splitTime = paramVal.Split(":");
                        //    if (splitTime.Length == 3)
                        //    {
                        //        var userInCore = CoreDb.User.Where(x => x.UserGuid == TheUser.UserGuid).FirstOrDefault();

                        //        int hr = int.Parse(splitTime[0]);
                        //        int mn = int.Parse(splitTime[1]);
                        //        int se = int.Parse(splitTime[2]);
                        //        DateTime tmpDT = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hr, mn, se);

                        //        var userTZ = TimeZoneInfo.FindSystemTimeZoneById(userInCore.Timezone);
                        //        var adjustedTime = TimeZoneInfo.ConvertTimeToUtc(tmpDT, userTZ);

                        //        paramVal = adjustedTime.Hour.ToString().PadLeft(2, '0') + ":"
                        //            + adjustedTime.Minute.ToString().PadLeft(2, '0') + ":"
                        //            + adjustedTime.Second.ToString().PadLeft(2, '0');
                        //    }
                        //}

                        par = new SqlParameter(paramName, paramType);
                        par.Value = TimeSpan.Parse(paramVal);
                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                    case "VarChar":
                        par = new SqlParameter(paramName, paramType);
                        if (paramVal == "--NULL--")
                        {
                            par.IsNullable = true;
                            par.Value = DBNull.Value;
                        }
                        else
                        {
                            par.Value = paramVal;
                        }
                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                    case "Date":
                        par = new SqlParameter(paramName, paramType);
                        if (paramVal == "")
                        {
                            par.IsNullable = true;
                            par.Value = DBNull.Value;
                        }
                        else
                        {
                            DateTime tryDT;
                            if (DateTime.TryParseExact(paramVal, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                            {
                                par.Value = tryDT;
                            }
                            else
                            {
                                par.Value = DateTime.Today;
                            }
                        }
                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                    case "DateTime":
                    case "DateTime2":
                        par = new SqlParameter(paramName, paramType);
                        if (paramVal == "")
                        {
                            par.IsNullable = true;
                            par.Value = DBNull.Value;
                        }
                        else
                        {
                            DateTime tryDT;
                            DateTime adjustedTime;

                            //var userInCore = CoreDb.User.Where(x => x.UserGuid == TheUser.UserGuid).FirstOrDefault();
                            //var userTZ = TimeZoneInfo.FindSystemTimeZoneById(userInCore.Timezone);
                            //var userTZ = TimeZoneInfo.FindSystemTimeZoneById(TheUser.Timezone);

                            if (DateTime.TryParseExact(paramVal, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                            {
                                switch (paramName)
                                {
                                    case "@updated":
                                    case "@_updated":
                                        // This will have been passed by the form controller already in UTC, so don't convert
                                        adjustedTime = tryDT;
                                        break;
                                    default:
                                        //par.Value = tryDT;
                                        //adjustedTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(tryDT, DateTimeKind.Unspecified), userTZ);
                                        adjustedTime = tryDT;

                                        break;
                                }
                                par.Value = adjustedTime;
                            }
                            else
                            {
                                // Invalid date, so set null
                                par.IsNullable = true;
                                par.Value = DBNull.Value;
                            }
                        }

                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                }
            }
        }
        public Dictionary<string, string> ExecuteUpdate(string idColumnName, string recordId)
        {
            var redir = "";
            bool firstParam = true;

            // Need to get original values for comparison and store an AuditFieldChange record per value
            var check = "SELECT ";
            firstParam = true;
            foreach (SqlParameter p in _sql.Parameters)
            {
                if (!firstParam) check += ",";
                check += "[" + p.ParameterName.Replace("@", "") + "]";
                firstParam = false;
            }
            check += " FROM [" + _tableName + "] WHERE [" + idColumnName + "]=" + recordId;
            _sql.CommandText = check;
            _con.Open();
            var sqlRet = _sql.ExecuteReader();

            int? whichAccount = null;
            if (_sql.Parameters.Contains("account_id"))
            {
                whichAccount = int.Parse(_sql.Parameters["account_id"].Value.ToString());
            }

            while (sqlRet.Read())
            {
                foreach (SqlParameter rp in _sql.Parameters)
                {
                    if ((rp.ParameterName.Replace("@", "") != "updatedby") &&
                        (rp.ParameterName.Replace("@", "") != "createdby") &&
                        (rp.ParameterName.Replace("@", "") != "_updatedby") &&
                        (rp.ParameterName.Replace("@", "") != "_createdby") &&
                        (rp.ParameterName.Replace("@", "") != "updated") &&
                        (rp.ParameterName.Replace("@", "") != "created") &&
                        (rp.Value != null))
                    {
                        var checkVal = sqlRet[rp.ParameterName.Replace("@", "")].ToString();
                        
                    }
                }
            }
            _con.Close();

            // Now actually update the record
            _sql.CommandText = "UPDATE [" + _tableName + "] SET ";

            firstParam = true;
            List<SqlParameter> toremove = new List<SqlParameter>();
            foreach (SqlParameter p in _sql.Parameters)
            {
                if (!firstParam) _sql.CommandText += ",";
                if (p.Value != null)
                {

                    _sql.CommandText += "[" + p.ParameterName.Replace("@", "") + "] = " + p.ParameterName;
                }
                else
                {
                    _sql.CommandText += "[" + p.ParameterName.Replace("@", "") + "] = NULL";
                    toremove.Add(p);
                }

                firstParam = false;
            }

            // Not actually required, but will tidy up the resulting SQL by removing unreferenced params
            foreach (var p in toremove)
            {
                _sql.Parameters.Remove(p);
            }

            _sql.CommandText += " WHERE [" + idColumnName + "] = " + recordId + ";";

            _con.Open();
            _sql.ExecuteScalar();
           
            _con.Close();

            Dictionary<string, string> retVal = new Dictionary<string, string>();
            retVal.Add("redirect", redir);
            retVal.Add("id", recordId);

            return retVal;
        }
        public Dictionary<string, string> ExecuteInsert()
        {
            var redir = "";
            bool firstParam = true;

            _sql.CommandText = "INSERT INTO [" + _tableName + "] (";
            foreach (SqlParameter p in _sql.Parameters)
            {
                if (!firstParam) _sql.CommandText += ",";
                _sql.CommandText += "[" + p.ParameterName.Replace("@", "") + "]";

                firstParam = false;
            }

            firstParam = true;
            _sql.CommandText += ") VALUES (";
            foreach (SqlParameter p in _sql.Parameters)
            {
                if (!firstParam) _sql.CommandText += ",";
                _sql.CommandText += p.ParameterName;

                firstParam = false;
            }

            _sql.CommandText += "); SELECT SCOPE_IDENTITY()";

            _con.Open();
            var sqlRet = _sql.ExecuteScalar(); //sql.ExecuteNonQuery();
            
            _con.Close();

            Dictionary<string, string> retVal = new Dictionary<string, string>();
            retVal.Add("redirect", redir);
            retVal.Add("id", sqlRet.ToString());

            return retVal;
        }
        private System.Data.SqlDbType GetDataType(string ColumnName)
        {
            switch (_columns[ColumnName])
            {
                case "int":
                    return System.Data.SqlDbType.Int;
                case "bigint":
                    return System.Data.SqlDbType.BigInt;
                case "decimal":
                    return System.Data.SqlDbType.Decimal;
                case "date":
                    return System.Data.SqlDbType.Date;
                case "time":
                    return System.Data.SqlDbType.Time;
                case "datetime2":
                    return System.Data.SqlDbType.DateTime2;
                case "datetime":
                    return System.Data.SqlDbType.DateTime;
                default:
                    return System.Data.SqlDbType.VarChar;
            }
        }
    }
}
