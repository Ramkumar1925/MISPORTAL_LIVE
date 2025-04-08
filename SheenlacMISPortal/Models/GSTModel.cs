namespace SheenlacMISPortal.Models
{
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
    public class AuthResponse
    {
        public string Status { get; set; }
        public List<ErrorDetail> ErrorDetails { get; set; }
        public List<InfoDtl> InfoDtls { get; set; }

        public data ?Data { get; set; }
    }
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
        public class data
            {
                public string ?ClientId { get; set; }
                public string ?UserName { get; set; }
                public string ?AuthToken { get; set; }
                public string ?Sek { get; set; }
                public string ?TokenExpiry { get; set; }

                public static implicit operator data(string v)
                {
                    throw new NotImplementedException();
                }
            }
        }
    

