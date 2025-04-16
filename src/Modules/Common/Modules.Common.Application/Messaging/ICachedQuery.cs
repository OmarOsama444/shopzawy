namespace Modules.Common.Application.Messaging
{
    public interface ICachedQuery
    {
        public string Key { get; }
        public TimeSpan? Expiration { get; }
    }
}
