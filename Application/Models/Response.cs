namespace Application.Models
{
    public class Response
    {
        public Response() { }
        public Response(bool succeeded, string message, string systemError, string title, string requestId)
        {
            Succeeded = succeeded;
            Message = message;
            SystemError = systemError;
            Title = title;
            RequestId = requestId;
        }
        public Response(bool succeeded, string message, string systemError, string title)
        {
            Succeeded = succeeded;
            Message = message;
            SystemError = systemError;
            Title = title;
        }
        public bool Succeeded { get; set; } = true;
        public string? Message { get; set; }
        public string? SystemError { get; set; }
        public string? Title { get; set; }
        public string? RequestId { get; set; }
        public string? RequestTime { get; set; }
        public string? ResponseTime { get; set; }

        public static Response Success()
        {
            return new Response(true, string.Empty, string.Empty, string.Empty, string.Empty);
        }
        public static Response Failure(string message, string systemError, string title)
        {
            return new Response(false, message, systemError, title, string.Empty);
        }
        public static Response Failure(string message, string systemError, string title, string requestId)
        {
            return new Response(false, message, systemError, title, requestId);
        }
    }
    public class Response<T> : Response
    {
        internal Response(bool succeeded, string message) : base(succeeded, message, string.Empty, string.Empty, string.Empty)
        {
        }
        internal Response(bool succeeded, string message, string systemError) : base(succeeded, message, systemError, string.Empty, string.Empty)
        {
        }
        internal Response(bool succeeded, string message, string systemError, string title) : base(succeeded, message, systemError, title, string.Empty)
        {
        }
        internal Response(bool succeeded, string message, string systemError, string title, string requestId) : base(succeeded, message, systemError, title, requestId)
        {
        }
        public static Response<T> Success(T data, string requestId = "")
        {
            var r = new Response<T>(true, string.Empty, string.Empty, string.Empty, requestId);
            r.Data = data;
            return r;
        }
        public T? Data { get; set; }
    }
}
