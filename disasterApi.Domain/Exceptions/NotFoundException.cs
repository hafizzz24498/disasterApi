namespace disasterApi.Domain.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string errorKey) : base(errorKey) { }
    }
}
