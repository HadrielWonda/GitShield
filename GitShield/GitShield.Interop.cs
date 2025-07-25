using System.Management.Automation;

public class CloudAvInteropLayer
{
    private readonly IPlatformServices _platform;

    public CloudAvInteropLayer(IPlatformServices platform) => _platform = platform;

    public T ExecuteCloudSafeOperation<T>(Func<T> operation)
    {
        try
        {
            if (_platform.IsCloudProcessActive())
                _platform.RequestCloudPause();

            _platform.RegisterAvExclusion();
            return operation();
        }
        finally
        {
            _platform.ResumeCloudSync();
        }
    }
}

// PowerShell integration for AV exclusion
public static class AntivirusManager
{
    public static void RegisterExclusion(string path)
    {
        using (var ps = PowerShell.Create())
        {
            ps.AddCommand("Add-MpPreference")
              .AddParameter("ExclusionPath", path);
            ps.Invoke();
        }
    }
}