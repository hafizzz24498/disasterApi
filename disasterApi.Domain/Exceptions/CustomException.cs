namespace disasterApi.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public string ErrorKey { get; }

        public CustomException(string errorKey)
            : base(errorKey)  // หรือ base(message) ก็ได้ถ้าต้องการ
        {
            ErrorKey = errorKey;
        }
    }
}
