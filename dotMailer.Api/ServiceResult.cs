namespace dotMailer.Api
{
    public class ServiceResult
    {
        public ServiceResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        public bool Success
        { get; private set; }

        public string Message
        { get; private set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(bool success, T data, string message = null)
            : base(success, message)
        {
            Data = data;
        }

        public T Data
        { get; private set; }
    }
}
