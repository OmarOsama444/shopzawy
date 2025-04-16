using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Modules.Users.Infrastructure.Options;

public class GmailSmtpOptions
{
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool SSL { get; set; }
    public string AppName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string SenderPassword { get; set; } = string.Empty;
}
