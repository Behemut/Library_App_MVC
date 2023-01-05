namespace Library_Web
{
    public static class SD
    {
        public static string? DefaultConnection { get; set; }
        public enum ApiType
        {         
            GET,
            POST,
            PUT,
            DELETE     
        }
    }
}
