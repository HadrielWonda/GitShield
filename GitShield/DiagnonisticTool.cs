using System.Text;

public class DiagnosticTool
{
    public void AnalyzeSystem()
    {
        var report = new StringBuilder();
        report.AppendLine($"Cloud processes: {_platform.DetectCloudProcesses()}");
        report.AppendLine($"AV exclusions: {CheckAvRegistration()}");
        report.AppendLine($"NTFS features: {VerifyNtfsSupport()}");
        // ETW event logging
        WindowsEventLog.Write(report.ToString());
    }
}