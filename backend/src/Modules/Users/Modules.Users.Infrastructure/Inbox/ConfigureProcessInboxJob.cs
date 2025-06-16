using Microsoft.Extensions.Options;
using Quartz;

namespace Modules.Users.Infrastructure.Inbox;

public class ConfigureProcessInboxJob(IOptions<InboxOptions> inBoxOptions) :
    IConfigureOptions<QuartzOptions>
{

    private readonly InboxOptions _inboxOptions = inBoxOptions.Value;
    public void Configure(QuartzOptions options)
    {
        string jobName = typeof(ProcessInboxJob).FullName!;
        var jobKey = new JobKey(jobName);

        options
        .AddJob<ProcessInboxJob>(configure => configure.WithIdentity(jobKey))
        .AddTrigger(configure =>
            configure.ForJob(jobKey)
            .WithSimpleSchedule(
                schedule => schedule.WithIntervalInSeconds(_inboxOptions.TimeSpanInSeconds)
                                    .RepeatForever()
            )
        );
    }
}

