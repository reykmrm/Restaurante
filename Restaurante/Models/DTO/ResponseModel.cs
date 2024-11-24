namespace Restaurante.Models.DTO
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public bool Warning { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ResponseModel(bool success, bool warning, string message, T data)
        {
            Success = success;
            Warning = warning;
            Message = message;
            Data = data;
        }

        public ResponseModel(bool success, bool warning, string message)
        {
            Success = success;
            Warning = warning;
            Message = message;
        }
    }
}
