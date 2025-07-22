namespace disasterApi.Domain.Exceptions
{
    public sealed class SystemErrorInternalServerErrorException : CustomException
    {
        public SystemErrorInternalServerErrorException(string errorKey) : base(errorKey)
        {
        }
    }
}
