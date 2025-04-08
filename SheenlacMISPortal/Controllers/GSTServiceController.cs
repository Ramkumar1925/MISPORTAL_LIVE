using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using SheenlacMISPortal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Net;
//using System.Net.Http.Formatting;
namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GSTServiceController : Controller
    {
        private readonly IConfiguration Configuration;

        public GSTServiceController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate()
        {
            try
            {
                string public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArxd93uLDs8HTPqcSPpxZrf0Dc29r3iPp0a8filjAyeX4RAH6lWm9qFt26CcE8ESYtmo1sVtswvs7VH4Bjg/FDlRpd+MnAlXuxChij8/vjyAwE71ucMrmZhxM8rOSfPML8fniZ8trr3I4R2o4xWh6no/xTUtZ02/yUEXbphw3DEuefzHEQnEF+quGji9pvGnPO6Krmnri9H4WPY0ysPQQQd82bUZCk9XdhSZcW/am8wBulYokITRMVHlbRXqu1pOFmQMO5oSpyZU3pXbsx+OxIOc4EDX0WMa9aH4+snt18WAXVGwF2B4fmBk7AtmkFzrTmbpmyVqA3KO2IjzMZPw0hQIDAQAB";
                //string public_key = GenerateRandomKey().ToString();
                string uri = "https://einv-apisandbox.nic.in/eivital/v1.04/auth";
                string clientId = "AASCS33TXPQSD2R";
                string clientSecret = "WKkYyD3Srg6u0ZOn42HV";
                string userName = "SHEENLAC_TN";
                string password = "SPLtn*3456";
                string gstin = "33AASCS5073J1ZV";

                byte[] aesKey = generateSecureKey();
                string strAesKey = Convert.ToBase64String(aesKey);

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("client-id", clientId);
                client.DefaultRequestHeaders.Add("client-secret", clientSecret);
                client.DefaultRequestHeaders.Add("gstin", gstin);

                var requestPayload = new RequestPayloadN();
                var authData = new Auth
                {
                    Password = password,
                    AppKey = strAesKey,
                    UserName = userName,
                    ForceRefreshAccessToken = false
                };

                string authStr = JsonConvert.SerializeObject(authData);
                byte[] authBytes = System.Text.Encoding.UTF8.GetBytes(authStr);
                requestPayload.Data = Encrypt(Convert.ToBase64String(authBytes), public_key);
                 requestPayload.Data = "mKmzPgI6V/Gj6vZxSv0vr3//SoRhNq2Ubt3tTJw9l+s3TuK8qFs41M8IP8BpX713pxxLzABq2CyUXNMjmq6ZGiA2XWM5Evn1zqczVfdrYeQEldv+fCohIWD4J6v0q9A1VUwuSgWy2QRk4WPV9HHyisLkl+IaVRggYGeZtxnh37vAdb5FeKgqA3zoiAYgR8OiuW7oylOAJicE8MKCmmkfjqMPt1iRYsYOREHduP8RmX6mBIAkMewX5QYKeUDe/XU/eP2XzZk38fbkltiWvioxHE4iRLwTRhevcxRA57Q4K0jM7z79vY78MfFKAE2Kiy4cOtB2sF4qsfQ8QNroNfPW3g==";

                //String requestBody = "{\"data\" : {\"UserName\": \"" + userName + "\",\"Password\": \"" + EnPassword + "\",    \"AppKey\": \"" + ENAPPKey + "\",   \"ForceRefreshAccessToken\":true }  }";

                HttpResponseMessage response = await client.PostAsJsonAsync(uri, requestPayload.Data);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Call is success");
                    string verification = await response.Content.ReadAsStringAsync();
                   // Console.WriteLine($"Response{verification}");
                    AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(verification);

                    // AuthResponse authResponse = await response.Content.Readasas<AuthResponse>();

                    // AuthResponse authResponse = await response.Content.ReadAsAsync<AuthResponse>(new[] { new JsonMediaTypeFormatter() });

                    // var str = (response1.Content.ReadAsStringAsync()).Result;
                    string sek = DecryptBySymmetricKey(authResponse.Data.Sek, aesKey);
                    Console.WriteLine($"Sek {sek}");

                    return Ok(new { Message = "Authentication successful", Sek = sek });
                }
                else
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();
                    string err = response.ReasonPhrase;
                    Console.WriteLine($"Error Response: {text} Reason: {err}");

                    return StatusCode((int)response.StatusCode, new { Error = text, Reason = err });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }

        private byte[] generateSecureKey()
        {
            Aes KEYGEN = Aes.Create();
            byte[] secretKey = KEYGEN.Key;
            return secretKey;
            throw new NotImplementedException();
        }

        static byte[] GenerateRandomKey()
        {
            const int keyLength = 32; // 32 bytes

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[keyLength];
                rng.GetBytes(key);
                return key;
            }
        }

        private string Encrypt(string data, string key)
        {
            byte[] keyBytes =
           Convert.FromBase64String(key); // your key here
                                          //byte[] keyBytes = "";

            AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes);
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            RSAParameters rsaParameters = new RSAParameters();
            rsaParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned();
            rsaParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);
            byte[] plaintext = Encoding.UTF8.GetBytes(data);
            byte[] ciphertext = rsa.Encrypt(plaintext, false);
            string cipherresult = Convert.ToBase64String(ciphertext);
            //string cipherresult = Encoding.ASCII.GetString(ciphertext);
            return cipherresult;
            throw new NotImplementedException();
        }

        private string DecryptBySymmetricKey(string encryptedText, byte[] key)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        [HttpPost("AuthenticateNew")]
        public async void Authenticate1(String _publickey, String _GstinNumber, String _username, String _Password, String _client_id, String _client_secret)
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Tls11;
                //| SecurityProtocolType.Ssl3;

            _publickey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArxd93uLDs8HTPqcSPpxZrf0Dc29r3iPp0a8filjAyeX4RAH6lWm9qFt26CcE8ESYtmo1sVtswvs7VH4Bjg/FDlRpd+MnAlXuxChij8/vjyAwE71ucMrmZhxM8rOSfPML8fniZ8trr3I4R2o4xWh6no/xTUtZ02/yUEXbphw3DEuefzHEQnEF+quGji9pvGnPO6Krmnri9H4WPY0ysPQQQd82bUZCk9XdhSZcW/am8wBulYokITRMVHlbRXqu1pOFmQMO5oSpyZU3pXbsx+OxIOc4EDX0WMa9aH4+snt18WAXVGwF2B4fmBk7AtmkFzrTmbpmyVqA3KO2IjzMZPw0hQIDAQAB";
            //string public_key = GenerateRandomKey().ToString();
            string uri = "https://einv-apisandbox.nic.in/eivital/v1.04/auth";
            _client_id = "AASCS33TXPQSD2R";
            _client_secret = "WKkYyD3Srg6u0ZOn42HV";
            //UserName = "SHEENLAC_TN";
            //string password = "SPLtn*3456";
            _GstinNumber = "33AASCS5073J1ZV";

            byte[] aesKey = generateSecureKey();
            string strAesKey = Convert.ToBase64String(aesKey);

            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("client-id", clientId);
            //client.DefaultRequestHeaders.Add("client-secret", clientSecret);
           // client.DefaultRequestHeaders.Add("gstin", gstin);
            String GSTNPublicKey = _publickey;
            String GSTNNumber = _GstinNumber;
            String GSTNClientID = _client_id;
            String GSTNClientSecret= _client_secret;
            String AppKey = strAesKey;
            String ENAPPKey = Encrypt(AppKey, GSTNPublicKey);
            String Password = "SPLtn*3456";
            String UserName = "SHEENLAC_TN";
            String EnPassword = Encrypt(Password,GSTNPublicKey);

            String requestBody = "{\"data\" : {\"UserName\": \""+UserName+ "\",\"Password\": \""+EnPassword+"\",    \"AppKey\": \""+ENAPPKey+"\",   \"ForceRefreshAccessToken\":true }  }";

           HttpResponseMessage response = await client.PostAsJsonAsync(uri, requestBody);
            String Aut_Data = response.Content.ReadAsStringAsync().Result;

            if (Aut_Data.Split(',')[0].Split(':')[1].Replace("\"","") == "1")
                {
                string token = Aut_Data.Split(',')[4].Split(':')[1].Replace("\"","");

            }
            
        }


        //static string EncryptAsymmetric(string data, string publicKey)
        //{
        //    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        //    {
        //        rsa.FromXmlString(publicKey);
        //        return rsa.Encrypt(data, false);
        //    }
        //}
    }
}
