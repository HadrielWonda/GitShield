public class GitShieldDaemon : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken token)
    {
        using var listener = new CloudSyncListener();
        while (!token.IsCancellationRequested)
        {
            await HandleFsEvents();
            await Task.Delay(100, token);
        }
    }
}