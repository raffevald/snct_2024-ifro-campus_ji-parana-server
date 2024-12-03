namespace Domain.Shared
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            Success = true;
            Timestamp = DateTime.UtcNow;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; }
        public int? StatusCode { get; set; }
    }
}
