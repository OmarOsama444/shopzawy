namespace Common.Domain.Exceptions
{
    public class ExpiredException : Exception
    {
        public ExpiredException(string code, string message) : base(message)
        {
            Code = code;
        }
        public string Code { get; }
    }
}
