namespace DTO.Models
{
    public class DataResponse
    {
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }
        public DataResponse(string message, bool isSucceeded)
        {
            Message = message;
            IsSucceeded = isSucceeded;
        }

        public object Data { get; set; }
    }

}
