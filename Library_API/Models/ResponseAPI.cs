namespace Library_API.Models
{
    public class ResponseAPI<T>
    {
        public bool IsSuccess { get; set; }

        public T Value { get; set; }

        public string Error { get; set; }

        public static ResponseAPI<T> Success(T value) => new ResponseAPI<T> { IsSuccess = true, Value = value };

        public static ResponseAPI<T> Failure(string error) => new ResponseAPI<T> { IsSuccess = false, Error = error };
    }
}
