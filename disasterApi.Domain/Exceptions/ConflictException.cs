namespace disasterApi.Domain.Exceptions
{
    public abstract class ConflictException : CustomException
    {
        protected ConflictException(string errorKey) : base(errorKey) { }
    }
}
