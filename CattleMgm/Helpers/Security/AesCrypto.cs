using System.Security.Cryptography;
namespace CattleMgm.Helpers.Security
{
    public static class AesCrypto
    {
        private static AesCryptoServiceProvider aesCrypto;

        public static string Enkrypt(int objectToBeEncrypted)
        {
            string iv = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesIV"];
            string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesKey"];
            aesCrypto = new AesCryptoServiceProvider();
            aesCrypto.Mode = CipherMode.CBC;
            aesCrypto.Padding = PaddingMode.PKCS7;
            aesCrypto.IV = Convert.FromBase64String(iv);
            aesCrypto.Key = Convert.FromBase64String(key);

            try
            {
                ICryptoTransform cryptoTransform = aesCrypto.CreateEncryptor();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(objectToBeEncrypted.ToString());
                        }
                        var result = Convert.ToBase64String(memoryStream.ToArray());
                        return result.Replace("+", "(").Replace("/", ")");
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static string EncryptString(string objectToBeEncrypted)
        {
            string iv = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesIV"];
            string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesKey"];
            aesCrypto = new AesCryptoServiceProvider();
            aesCrypto.Mode = CipherMode.CBC;
            aesCrypto.Padding = PaddingMode.PKCS7;
            aesCrypto.IV = Convert.FromBase64String(iv);
            aesCrypto.Key = Convert.FromBase64String(key);

            try
            {
                ICryptoTransform cryptoTransform = aesCrypto.CreateEncryptor();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(objectToBeEncrypted.Replace("_", " "));
                        }
                        var result = Convert.ToBase64String(memoryStream.ToArray());
                        return result.Replace("+", "(").Replace("/", ")");
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static string Ecrypt<T>(T objectToEncrypt)
        {
            string iv = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesIV"];
            string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesKey"];
            aesCrypto = new AesCryptoServiceProvider();
            aesCrypto.Mode = CipherMode.CBC;
            aesCrypto.Padding = PaddingMode.PKCS7;
            aesCrypto.IV = Convert.FromBase64String(iv);
            aesCrypto.Key = Convert.FromBase64String(key);

            try
            {
                ICryptoTransform cryptoTransform = aesCrypto.CreateEncryptor();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(objectToEncrypt);
                        }
                        var result = Convert.ToBase64String(memoryStream.ToArray()).Replace("+", "(").Replace("/", ")");
                        return result;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static T Decrypt<T>(string objectToBeDecrypted)
        {
            if (objectToBeDecrypted != null)
            {
                try
                {
                    objectToBeDecrypted = objectToBeDecrypted.Replace("(", "+").Replace(")", "/").Replace(" ", "+").Replace("%2F", "/");
                    string iv = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesIV"];
                    string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SecurityConfig")["AesKey"];
                    aesCrypto = new AesCryptoServiceProvider();
                    aesCrypto.Mode = CipherMode.CBC;
                    aesCrypto.Padding = PaddingMode.PKCS7;
                    aesCrypto.IV = Convert.FromBase64String(iv);
                    aesCrypto.Key = Convert.FromBase64String(key);
                    ICryptoTransform cryptoTransform = aesCrypto.CreateDecryptor();

                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(objectToBeDecrypted)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                if (typeof(T) == typeof(int))
                                {
                                    var txt = streamReader.ReadToEnd();
                                    return (T)Convert.ChangeType(Convert.ToInt32(txt), typeof(T));
                                }
                                else
                                {
                                    return (T)Convert.ChangeType(streamReader.ReadToEnd(), typeof(T));
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return (T)Convert.ChangeType(0, typeof(T));
                }
            }
            else
            {
                return (T)Convert.ChangeType(0, typeof(T));
            }
        }

        public static void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                string password = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["AesKeyFile"];
                byte[] key = Convert.FromBase64String(password);
                byte[] iv = Convert.FromBase64String(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["AesIvFile"]);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, iv),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);

                fsIn.Close();
                cs.Close();

                fsCrypt.Flush();
                fsCrypt.Close();
            }
            catch { }
        }

        public static byte[] DecryptFile(string inputFile)
        {
            string password = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["AesKeyFile"];
            byte[] key = Convert.FromBase64String(password);
            byte[] iv = Convert.FromBase64String(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["AesIvFile"]);

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            MemoryStream memoryStream = new MemoryStream();

            int data;
            while ((data = cs.ReadByte()) != -1)
                memoryStream.WriteByte((byte)data);

            return memoryStream.ToArray();
        }

       
    }
}
