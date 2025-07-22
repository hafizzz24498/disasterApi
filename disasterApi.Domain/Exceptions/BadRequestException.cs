namespace disasterApi.Domain.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string errorKey) : base(errorKey)
        { }
    }
}
