using System.Security.Cryptography;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class GSTToken
    {
        public static byte[] generateSecureKey()
        {
            Aes KEYGEN = Aes.Create();
            byte[] secretKey = KEYGEN.Key;
            return secretKey;
        }
        public string Encryptword(string Encryptval, string key)
        {
            byte[] SrctArray;
            byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(Encryptval);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
            SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objcrpt.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateEncryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);
            objt.Clear();
            return Convert.ToBase64String(resArray, 0, resArray.Length);
        }
        public class AuthResponse
        {
            public string Status { get; set; }
            public List<ErrorDetail> ErrorDetails { get; set; }
            public List<InfoDtl> InfoDtls { get; set; }
            public class ErrorDetail
            {
                public string ErrorCode { get; set; }
                public string ErrorMessage { get; set; }
            }
            public class InfoDtl
            {
                public string InfCd { get; set; }
                public List<Infodata> Desc { get; set; }
            }
            public class Infodata
            {
                public string ErrorCode { get; set; }
                public string ErrorMessage { get; set; }
            }
            public data Data { get; set; }
            public class data
            {
                public string ClientId { get; set; }
                public string UserName { get; set; }
                public string AuthToken { get; set; }
                public string Sek { get; set; }
                public string TokenExpiry { get; set; }
                public static implicit operator data(string v)
                {
                    throw new NotImplementedException();
                }
            }
        }


        public static string DecryptBySymmerticKey(string encryptedText, byte[] key)
        {

            byte[] dataToDecrypt = Convert.FromBase64String(encryptedText);
            var keyBytes = key;
            AesManaged tdes = new AesManaged();
            tdes.KeySize = 256;
            tdes.BlockSize = 128;
            tdes.Key = keyBytes;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform decrypt__1 = tdes.CreateDecryptor();
            byte[] deCipher = decrypt__1.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            tdes.Clear();
            string EK_result = Convert.ToBase64String(deCipher);
            // var EK = Convert.FromBase64String(EK_result);
            // return EK;
            return EK_result;

        }

        public class Auth
        {
            public string Password { get; set; }
            public string AppKey { get; set; }
            public string UserName { get; set; }
            public Boolean ForceRefreshAccessToken { get; set; }
        }
        public class RequestPayloadN
        {
            public string Data { get; set; }
        }
    }
}
