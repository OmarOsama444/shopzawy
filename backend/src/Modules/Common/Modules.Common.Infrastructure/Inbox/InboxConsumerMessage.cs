namespace Modules.Common.Infrastructure.Inbox;

public class InboxConsumerMessage
{
    public Guid id { get; set; }
    public string HandlerName { get; set; } = string.Empty;
}
