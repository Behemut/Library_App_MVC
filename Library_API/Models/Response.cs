namespace Library_API.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public object Result { get; set; }

        public string DisplayMessage { get; set; }

        public List<string> ErrorMessage { get; set; }
    }
}
