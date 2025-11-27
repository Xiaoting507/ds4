namespace CalculadoraWebAPI.Models
{

    public class ApiResponse<T>
    {

        public bool Success { get; set; }

        public string Message { get; set; }

        public int Count { get; set; }

        public T Data { get; set; }
    }


    public class ErrorResponse
    {

        public bool Success { get; set; }

        public string Message { get; set; }

        public string Error { get; set; }
    }
}
