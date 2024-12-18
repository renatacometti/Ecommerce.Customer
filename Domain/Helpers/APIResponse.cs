namespace Domain.Helpers
{
    public class APIResponse<T>
    {
        public bool Success {get; set; }
        public bool Error { get; set; }

        public string Message { get; set; }
        public T Item { get; set; }
    }
}
