using System.Text;
using System.Security.Cryptography;

namespace Common.Common
{
    public class SymCryptography
    {
        #region Private members...
        private string mKey = string.Empty;
        private string mSalt = string.Empty;
        private ServiceProviderEnum mAlgorithm;
        private SymmetricAlgorithm mCryptoService;

        private void SetLegalIV()
        {
            #region
            // Set symmetric algorithm
            switch (mAlgorithm)
            {
                case ServiceProviderEnum.Rijndael:
                    //mCryptoService.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    mCryptoService.IV = new byte[] { 15, 111, 19, 46, 53, 194, 205, 249, 5, 70, 156, 234, 168, 75, 115, 204 };
                    break;
                default:
                    //mCryptoService.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    mCryptoService.IV = new byte[] { 15, 111, 19, 46, 53, 194, 205, 249 };
                    break;
            }
            #endregion
        }
        #endregion

        #region Public interfaces...
        public enum ServiceProviderEnum : int
        {
            #region
            // Supported service providers
            Rijndael,
            RC2,
            DES,
            TripleDES
            #endregion
        }

        public SymCryptography()
        {
            #region
            // Default symmetric algorithm
            mCryptoService = new RijndaelManaged();
            mCryptoService.Mode = CipherMode.CBC;
            mAlgorithm = ServiceProviderEnum.Rijndael;
            #endregion
        }

        public SymCryptography(ServiceProviderEnum serviceProvider)
        {
            #region
            // Select symmetric algorithm
            switch (serviceProvider)
            {
                case ServiceProviderEnum.Rijndael:
                    mCryptoService = new RijndaelManaged();
                    mAlgorithm = ServiceProviderEnum.Rijndael;
                    break;
                case ServiceProviderEnum.RC2:
                    mCryptoService = new RC2CryptoServiceProvider();
                    mAlgorithm = ServiceProviderEnum.RC2;
                    break;
                case ServiceProviderEnum.DES:
                    mCryptoService = new DESCryptoServiceProvider();
                    mAlgorithm = ServiceProviderEnum.DES;
                    break;
                case ServiceProviderEnum.TripleDES:
                    mCryptoService = new TripleDESCryptoServiceProvider();
                    mAlgorithm = ServiceProviderEnum.TripleDES;
                    break;
            }
            mCryptoService.Mode = CipherMode.CBC;
            #endregion
        }

        public SymCryptography(string serviceProviderName)
        {
            #region
            try
            {
                // Select symmetric algorithm
                switch (serviceProviderName.ToLower())
                {
                    case "rijndael":
                        serviceProviderName = "Rijndael";
                        mAlgorithm = ServiceProviderEnum.Rijndael;
                        break;
                    case "rc2":
                        serviceProviderName = "RC2";
                        mAlgorithm = ServiceProviderEnum.RC2;
                        break;
                    case "des":
                        serviceProviderName = "DES";
                        mAlgorithm = ServiceProviderEnum.DES;
                        break;
                    case "tripledes":
                        serviceProviderName = "TripleDES";
                        mAlgorithm = ServiceProviderEnum.TripleDES;
                        break;
                }

                // Set symmetric algorithm
                mCryptoService = (SymmetricAlgorithm)CryptoConfig.CreateFromName(serviceProviderName);
                mCryptoService.Mode = CipherMode.CBC;
            }
            catch
            {
                throw;
            }
            #endregion
        }

        public virtual byte[] GetLegalKey()
        {
            #region
            // Adjust key if necessary, and return a valid key
            if (mCryptoService.LegalKeySizes.Length > 0)
            {
                // Key sizes in bits
                int keySize = mKey.Length * 8;
                int minSize = mCryptoService.LegalKeySizes[0].MinSize;
                int maxSize = mCryptoService.LegalKeySizes[0].MaxSize;
                int skipSize = mCryptoService.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Extract maximum size allowed
                    mKey = mKey.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Set valid size
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Pad the key with asterisk to make up the size
                        mKey = mKey.PadRight(validSize / 8, '*');
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(mKey, ASCIIEncoding.ASCII.GetBytes(mSalt));
            return key.GetBytes(mKey.Length);
            #endregion
        }

        public virtual string Encrypt(string plainText)
        {
            #region
            byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(plainText);
            byte[] keyByte = GetLegalKey();

            // Set private key
            mCryptoService.Key = keyByte;
            SetLegalIV();

            // Encryptor object
            ICryptoTransform cryptoTransform = mCryptoService.CreateEncryptor();

            // Memory stream object
            MemoryStream ms = new MemoryStream();

            // Crpto stream object
            CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);

            // Write encrypted byte to memory stream
            cs.Write(plainByte, 0, plainByte.Length);
            cs.FlushFinalBlock();

            // Get the encrypted byte length
            byte[] cryptoByte = ms.ToArray();

            // Convert into base 64 to enable result to be used in Xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
            #endregion
        }

        public virtual string Decrypt(string cryptoText)
        {
            #region
            // Convert from base 64 string to bytes
            byte[] cryptoByte = Convert.FromBase64String(cryptoText);
            byte[] keyByte = GetLegalKey();

            // Set private key
            mCryptoService.Key = keyByte;
            SetLegalIV();

            // Decryptor object
            ICryptoTransform cryptoTransform = mCryptoService.CreateDecryptor();
            try
            {
                #region
                // Memory stream object
                MemoryStream ms = new MemoryStream(cryptoByte, 0, cryptoByte.Length);

                // Crpto stream object
                CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read);

                // Get the result from the Crypto stream
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
                #endregion
            }
            catch { throw; }
            #endregion
        }

        public string Key
        {
            #region
            get { return mKey; }
            set { mKey = value; }
            #endregion
        }
        public string Salt
        {
            #region
            get { return mSalt; }
            set { mSalt = value; }
            #endregion
        }
        #endregion
    }

    public class Hash
    {
        private string mSalt;
        private HashAlgorithm mCryptoService;

        #region Public interfaces...
        public enum ServiceProviderEnum : int
        {
            SHA1,
            SHA256,
            SHA384,
            SHA512,
            MD5
        }

        public Hash()
        {
            #region
            mCryptoService = new SHA1Managed();
            #endregion
        }

        public Hash(ServiceProviderEnum serviceProvider)
        {
            #region
            // Select hash algorithm
            switch (serviceProvider)
            {
                case ServiceProviderEnum.MD5:
                    mCryptoService = new MD5CryptoServiceProvider();
                    break;
                case ServiceProviderEnum.SHA1:
                    mCryptoService = new SHA1Managed();
                    break;
                case ServiceProviderEnum.SHA256:
                    mCryptoService = new SHA256Managed();
                    break;
                case ServiceProviderEnum.SHA384:
                    mCryptoService = new SHA384Managed();
                    break;
                case ServiceProviderEnum.SHA512:
                    mCryptoService = new SHA512Managed();
                    break;
            }
            #endregion
        }

        public Hash(string serviceProviderName)
        {
            #region
            try
            {
                mCryptoService = (HashAlgorithm)CryptoConfig.CreateFromName(serviceProviderName.ToUpper());
            }
            catch
            {
                throw;
            }
            #endregion
        }

        public virtual string Encrypt(string plainText)
        {
            #region
            byte[] cryptoByte = mCryptoService.ComputeHash(ASCIIEncoding.ASCII.GetBytes(plainText + mSalt));

            // Convert into base 64 to enable result to be used in Xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.Length);
            #endregion
        }

        public string Salt
        {
            #region
            get { return mSalt; }
            set { mSalt = value; }
            #endregion
        }
        #endregion
    }
    public static class UniqueName
    {
        public static string GetUniqueName()
        {
            #region
            return new Guid().ToString("N");
            #endregion
        }
        public static string GetUniqueName(int length)
        {
            #region
            return new Guid().ToString("N").Substring(0, length);
            #endregion
        }
    }
}
