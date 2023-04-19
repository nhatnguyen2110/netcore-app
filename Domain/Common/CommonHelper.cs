using NETCore.Encrypt;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// Build Email Content
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static MailMessage BuildMailMessage(string? displayName, string fromAddress, string toAddress, string subject, string body)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromAddress, displayName),
                Subject = subject,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
            };

            string[] tos = toAddress.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string to in tos)
                message.To.Add(new MailAddress(to));

            return message;
        }
        /// <summary>
        /// Send Mail
        /// </summary>
        /// <param name="host"></param>
        /// <param name="smtpPortConfig"></param>
        /// <param name="smtpSSLConfig"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="emailFrom"></param>
        /// <param name="displayName"></param>
        /// <param name="subject"></param>
        /// <param name="emailContent"></param>
        /// <param name="emailTo"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="attachments"></param>
        public static void SendMail(string host, string smtpPortConfig, string smtpSSLConfig, string username, string password, string emailFrom, string? displayName, string subject, string emailContent, string emailTo, string? cc, string? bcc, params Attachment[]? attachments)
        {
            // Get Email Settings
            int port = int.Parse(smtpPortConfig);
            bool enableSSL = bool.Parse(smtpSSLConfig);

            MailMessage mailMessage = BuildMailMessage(displayName, emailFrom, emailTo, subject, emailContent);

            if (!String.IsNullOrEmpty(cc))
            {
                string[] arr_cc = cc.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string _cc in arr_cc)
                {
                    MailAddress copy = new MailAddress(_cc);
                    mailMessage.CC.Add(copy);
                }

            }
            if (!String.IsNullOrEmpty(bcc))
            {
                string[] arr_bcc = bcc.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var _bcc in arr_bcc)
                {
                    MailAddress mail = new MailAddress(_bcc);
                    mailMessage.Bcc.Add(mail);
                }
            }
            if (attachments != null && attachments.Length > 0)
            {
                foreach (var attach in attachments)
                {
                    mailMessage.Attachments.Add(attach);
                }
            }
            SmtpClient smtp = new SmtpClient { Host = host, Port = port };

            NetworkCredential credential = new NetworkCredential(username, password);
            smtp.Credentials = credential;
            smtp.EnableSsl = enableSSL;
            smtp.Send(mailMessage);
            mailMessage.Dispose();
        }
        /// <summary>
        /// Sets a property on an object to a value.
        /// </summary>
        /// <param name="instance">The object whose property to set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            var instanceType = instance.GetType();
            var pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new Exception($"No property '{propertyName}' found on the instance of type '{instanceType}'.");
            if (!pi.CanWrite)
                throw new Exception($"The property '{propertyName}' on the instance of type '{instanceType}' does not have a setter.");
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }
        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return To(value, destinationType, CultureInfo.InvariantCulture);
#pragma warning restore CS8603 // Possible null reference return.
        }
        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object? To(object value, Type destinationType, CultureInfo culture)
        {
            if (value == null)
                return null;

            var sourceType = value.GetType();

            var destinationConverter = TypeDescriptor.GetConverter(destinationType);
            if (destinationConverter.CanConvertFrom(value.GetType()))
                return destinationConverter.ConvertFrom(null, culture, value);

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter.CanConvertTo(destinationType))
                return sourceConverter.ConvertTo(null, culture, value, destinationType);

            if (destinationType.IsEnum && value is int)
                return Enum.ToObject(destinationType, (int)value);

            if (!destinationType.IsInstanceOfType(value))
                return Convert.ChangeType(value, destinationType, culture);

            return value;
        }
        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return (T)To(value, typeof(T));
        }
        public static string RSAEncrypt(string plainText, string publicKeyEncode)
        {
            var result = string.Empty;

            var hexString = EncryptProvider.RSAEncrypt(publicKeyEncode, plainText, RSAEncryptionPadding.Pkcs1, true);
            byte[] resultantArray = new byte[hexString.Length / 2];
            for (int i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            result = Convert.ToBase64String(resultantArray);

            return result;
        }
        public static string RSADecrypt(string cipherText, string privateKeyEncode)
        {
            var result = string.Empty;

            cipherText = cipherText.Replace("\r\n", string.Empty);
            byte[] bytes = Convert.FromBase64String(cipherText);
            string hex = BitConverter.ToString(bytes).Replace("-", string.Empty);
            result = EncryptProvider.RSADecrypt(privateKeyEncode, hex, RSAEncryptionPadding.Pkcs1, true);

            return result;
        }
        private static readonly byte[] _salt = new byte[] { 0x23, 0x34, 0x64, 0xee, 0xe2, 0x4d, 0xF5, 0x64, 0x76, 0x15, 0x54, 0x65, 0x76 };

        public static string Encrypt(string payload, string encryptionKey)
        {
            string result = String.Empty;
            try
            {
                byte[] clearBytes = Encoding.UTF8.GetBytes(payload);
#pragma warning disable SYSLIB0022 // Type or member is obsolete
                using (RijndaelManaged encryptor = new RijndaelManaged())
                {
                    encryptor.KeySize = 256;
                    encryptor.BlockSize = 128;

                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(encryptionKey, _salt);
                    encryptor.Key = key.GetBytes(encryptor.KeySize / 8);
                    encryptor.IV = key.GetBytes(encryptor.BlockSize / 8);
                    encryptor.Mode = CipherMode.CBC;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        result = Convert.ToBase64String(ms.ToArray());
                    }
                }
#pragma warning restore SYSLIB0022 // Type or member is obsolete
            }
            catch (Exception ex)
            {
#pragma warning disable CA2200 // Rethrow to preserve stack details
                throw ex;
#pragma warning restore CA2200 // Rethrow to preserve stack details
            }
            return result;
        }

        public static string Decrypt(string cipherTxt, string encryptionKey)
        {
            string result = String.Empty;
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherTxt);

#pragma warning disable SYSLIB0022 // Type or member is obsolete
                using (RijndaelManaged encryptor = new RijndaelManaged())
                {
                    encryptor.KeySize = 256;
                    encryptor.BlockSize = 128;

                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(encryptionKey, _salt);
                    encryptor.Key = key.GetBytes(encryptor.KeySize / 8);
                    encryptor.IV = key.GetBytes(encryptor.BlockSize / 8);
                    encryptor.Mode = CipherMode.CBC;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        result = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
#pragma warning restore SYSLIB0022 // Type or member is obsolete
            }
            catch (Exception ex)
            {
#pragma warning disable CA2200 // Rethrow to preserve stack details
                throw ex;
#pragma warning restore CA2200 // Rethrow to preserve stack details
            }
            return result;
        }
    }
}
