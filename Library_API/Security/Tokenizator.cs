namespace Library_API.Security
{
    public class Tokenizator
    {

        readonly static string secretKey = "VypF9njaQUDwx9BvH6ANT5MbmyNHZ3uH";
        readonly static string audienceToken = "*";
        readonly static string issuerToken = "*";
        readonly static int expireTimeMinutes = 10;
        readonly static int expireExternalTime = 30;
        readonly static DateTime expirationTime = DateTime.Now.AddMinutes(expireTimeMinutes);
        readonly static int refreshTokenValidityInDays = 1;


        public static string SecretKey { get => secretKey; }
        public static string AudienceToken { get => audienceToken; }
        public static string IssuerToken { get => issuerToken; }
        public static int ExpireTimeMinutes { get => expireTimeMinutes; }

        public static int ExpireExternalTime { get => expireExternalTime; }
        public static DateTime ExpirationTime { get => expirationTime; }
        public static int RefreshTokenValidityInDays { get => refreshTokenValidityInDays; }

    }
}
