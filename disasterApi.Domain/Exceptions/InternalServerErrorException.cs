namespace disasterApi.Domain.Exceptions
{
    public abstract class InternalServerErrorException : CustomException
    {
        public InternalServerErrorException(string errorKey) : base(errorKey) { }
    }
}
