using static Library_Web.SD;

namespace Library_Web.Models.System
{
    public class APIRequest
    {
        public ApiType apiType { get; set; }
        public string? Url { get; set; }

        public object? Data { get; set; }

        public string? Token { get; set; }

    }
}
