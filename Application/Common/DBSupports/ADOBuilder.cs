using Application.Common.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Application.Common.DBSupports
{
    public class ADOBuilder
    {
        IApplicationDbContext _applicationDbContext;
        Dictionary<string, string> _columns;
        string _tableName;
        string _currentUserId;
        SqlConnection _con;
        SqlCommand _sql;
        public ADOBuilder(IApplicationDbContext applicationDbContext, string tableName, string currentUserId, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _tableName = tableName;
            _currentUserId = currentUserId;
            var tab = new SqlParameter("tab", _tableName);
            var cols = _applicationDbContext.GetSPColumnTypes.FromSqlRaw("SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@tab", tab);
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
                    case "Money":
                    case "Float":
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
                        if (paramVal != "")
                        {
                            DateTime tryDT;
                            if (DateTime.TryParseExact(paramVal, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                            {
                                par.Value = tryDT;
                            }
                            else if (DateTime.TryParseExact(paramVal, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                            {
                                par.Value = tryDT;
                            }
                            else
                            {
                                par.IsNullable = true;
                                par.Value = DBNull.Value;
                            }
                        }
                        else
                        {
                            par.IsNullable = true;
                            par.Value = DBNull.Value;
                        }
                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                    case "DateTimeOffset":
                        par = new SqlParameter(paramName, paramType);
                        if (paramVal == "")
                        {
                            par.IsNullable = true;
                            par.Value = DBNull.Value;
                        }
                        else
                        {
                            DateTimeOffset tryDT;

                            if (paramName.EndsWith("UTC"))
                            {
                                if (DateTimeOffset.TryParseExact(paramVal, "d/M/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out tryDT))
                                {
                                    par.Value = tryDT;
                                }
                                else if (DateTimeOffset.TryParseExact(paramVal, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out tryDT))
                                {
                                    par.Value = tryDT;
                                }
                                else
                                {
                                    // Invalid date, so set null
                                    par.IsNullable = true;
                                    par.Value = DBNull.Value;
                                }
                            }
                            else
                            {
                                if (DateTimeOffset.TryParseExact(paramVal, "d/M/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                                {
                                    par.Value = tryDT;
                                }
                                else if (DateTimeOffset.TryParseExact(paramVal, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                                {
                                    par.Value = tryDT;
                                }
                                else
                                {
                                    // Invalid date, so set null
                                    par.IsNullable = true;
                                    par.Value = DBNull.Value;
                                }
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

                            if (paramName.EndsWith("UTC"))
                            {
                                if (DateTime.TryParseExact(paramVal, "d/M/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                                {
                                    par.Value = tryDT.ToUniversalTime();
                                }
                                else if (DateTime.TryParseExact(paramVal, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                                {
                                    par.Value = tryDT.ToUniversalTime();
                                }
                                else
                                {
                                    // Invalid date, so set null
                                    par.IsNullable = true;
                                    par.Value = DBNull.Value;
                                }
                            }
                            else
                            {
                                if (DateTime.TryParseExact(paramVal, "d/M/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                                {
                                    par.Value = tryDT;
                                }
                                else if (DateTime.TryParseExact(paramVal, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out tryDT))
                                {
                                    par.Value = tryDT;
                                }
                                else
                                {
                                    // Invalid date, so set null
                                    par.IsNullable = true;
                                    par.Value = DBNull.Value;
                                }
                            }
                        }

                        if (_sql.Parameters.Contains(paramName)) _sql.Parameters.RemoveAt(paramName); // Remove if already there
                        _sql.Parameters.Add(par);
                        break;
                }
            }
        }
        /// <summary>
        /// return number of updated records
        /// </summary>
        /// <param name="idColumnName"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public int ExecuteUpdate(string idColumnName, string recordId)
        {
            bool firstParam = true;
            _sql.CommandText = "UPDATE [" + _tableName + "] SET ";
            // add last modified by, last modified at UTC
            var paramLastModifiedBy = "LastModifiedBy";
            var paramLastModifiedAtUTC = "LastModifiedAtUTC";
            if (_columns.Any(x => x.Key == paramLastModifiedBy))
            {
                this.AddParam(paramLastModifiedBy, _currentUserId);
            }
            if (_columns.Any(x => x.Key == paramLastModifiedAtUTC))
            {
                this.AddParam(paramLastModifiedAtUTC, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            }
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
            _sql.CommandText += " WHERE [" + idColumnName + "] = @" + idColumnName + ";";
            this.AddParam(idColumnName, recordId);
            // Not actually required, but will tidy up the resulting SQL by removing unreferenced params
            foreach (var p in toremove)
            {
                _sql.Parameters.Remove(p);
            }
            try
            {
                _con.Open();
                return _sql.ExecuteNonQuery();
            }
            finally
            {
                _con.Close();
            }
            
        }
        /// <summary>
        /// return inserted record Id
        /// </summary>
        /// <returns></returns>
        public string ExecuteInsert()
        {
            bool firstParam = true;
            // check if table has identity Id
            bool isIdentityId = GetDataType("Id") == System.Data.SqlDbType.VarChar;
            var newId = string.Empty;
            if (isIdentityId)
            {
                // if user don't pass param Id then auto create a guid Id
                if (_sql.Parameters.Contains("@Id"))
                {
                    newId = _sql.Parameters["@Id"].Value.ToString();
                }
                if (String.IsNullOrEmpty(newId))
                {
                    newId = Guid.NewGuid().ToString();
                    this.AddParam("Id", newId);
                }
            }

            // add created by, created at UTC
            var paramCreatedBy = "CreatedBy";
            var paramCreatedAtUTC = "CreatedAtUTC";
            if (_columns.Any(x=>x.Key == paramCreatedBy))
            {
                this.AddParam(paramCreatedBy, _currentUserId);
            }
            if (_columns.Any(x => x.Key == paramCreatedAtUTC))
            {
                this.AddParam(paramCreatedAtUTC, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            }
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
            if (isIdentityId)
            {
                _sql.CommandText += ");";
            }
            else
            {
                _sql.CommandText += "); SELECT SCOPE_IDENTITY()";
            }
            
            try
            {
                _con.Open();
                if (isIdentityId)
                {
                    _sql.ExecuteNonQuery();
                    return newId;
                }
                else
                {
                    return "" + _sql.ExecuteScalar();
                }
            }
            finally
            {
                _con.Close();
            }
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
                case "datetimeoffset":
                    return System.Data.SqlDbType.DateTimeOffset;
                case "money":
                    return System.Data.SqlDbType.Money;
                case "float":
                    return System.Data.SqlDbType.Float;
                default:
                    return System.Data.SqlDbType.VarChar;
            }
        }
    }
}
