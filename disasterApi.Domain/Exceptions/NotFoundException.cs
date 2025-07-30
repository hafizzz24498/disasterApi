namespace disasterApi.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string errorKey) : base(errorKey) { }
    }
}
