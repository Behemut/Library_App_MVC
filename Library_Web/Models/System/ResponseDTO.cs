
namespace Library_Web.Models.System
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public string DisplayMessage { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
