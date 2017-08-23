using System;
using System.Data.SqlClient;
using MIC.Database.Connection.Models.Interfaces;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace MIC.Database.Connection.Models
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public const int TimeoutUndefinedValue = 30;
        public const string DbDefaultName = "AnyForm";

        public string ServerName { get; set; }
        public string Instance { get; set; }
        public string DbName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public int ConnectionTimeout { get; set; }
        public int CommandTimeout { get; set; }
        public string IsolationLevel { get; set; }

        public DatabaseSetting()
        {
            ServerName = "";
            Instance = "";
            DbName = DbDefaultName;
            UserId = GetUserId();
            Password = GetConfidencialWord();
            ConnectionTimeout = TimeoutUndefinedValue;
            CommandTimeout = TimeoutUndefinedValue;
            IsolationLevel = "";
        }

        /// <summary>
        /// 接続文字列を取得
        /// </summary>
        /// <returns></returns>
        public string ToConnectionString()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = string.IsNullOrEmpty(Instance) ? ServerName : string.Format("{0}\\{1}", ServerName, Instance),
                InitialCatalog = DbName,
                UserID = UserId,
                Password = Password,
                ConnectTimeout = ConnectionTimeout
            }.ToString();
        }

        /// <summary>
        /// Userを設定
        /// </summary>
        /// <returns></returns>
        private string GetUserId()
        {
            return Encryptor.Decrypt("QCUB+ODLXMo=");
        }

        /// <summary>
        /// Confidencialを設定
        /// </summary>
        /// <returns></returns>
        public string GetConfidencialWord()
        {
            return Encryptor.Decrypt("Ql0lUkVg/Y4DC4tirMHZREeCaRtOj3ed");
        }
    }

    public class Encryptor
    {
        // 鍵
        private const string Key = "123456789ABCDEF123454678";
        // 初期化ベクタ
        private const string Vector = "12345678";

        /// <summary>
        /// 暗号化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            var src = Encoding.UTF8.GetBytes(str);
            var desKey = Encoding.UTF8.GetBytes(Key);
            var desIv = Encoding.UTF8.GetBytes(Vector);

            var des = new TripleDESCryptoServiceProvider();
            var ms = new MemoryStream();

            var cs = new CryptoStream(ms, des.CreateEncryptor(desKey, desIv), CryptoStreamMode.Write);
            cs.Write(src, 0, src.Length);
            cs.Close();

            var cryptoData = ms.ToArray();
            ms.Close();

            return Convert.ToBase64String(cryptoData);
        }

        /// <summary>
        /// 復号化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            var src = Convert.FromBase64String(str);
            var desKey = Encoding.UTF8.GetBytes(Key);
            var desIv = Encoding.UTF8.GetBytes(Vector);

            var des = new TripleDESCryptoServiceProvider();
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(desKey, desIv), CryptoStreamMode.Write);
            cs.Write(src, 0, src.Length);
            cs.Close();

            var cryptoData = ms.ToArray();
            ms.Close();

            return Encoding.UTF8.GetString(cryptoData);
        }
    }

}

