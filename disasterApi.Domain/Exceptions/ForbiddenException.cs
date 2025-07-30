namespace disasterApi.Domain.Exceptions
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string errorKey) : base(errorKey) { }
    }
}
