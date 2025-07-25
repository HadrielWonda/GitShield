using System.IO.MemoryMappedFiles;

public static class NtfsFeatures
{
    // Store metadata in alternate data streams
    public static void WriteMetadata(string filePath, string streamName, byte[] data)
    {
        string fullPath = $"{filePath}:{streamName}";
        File.WriteAllBytes(fullPath, data);
    }

    // Memory-mapped operations with secure memory
    public static unsafe void SecureMemoryMap(string path)
    {
        using (var mmf = MemoryMappedFile.CreateFromFile(path))
        using (var view = mmf.CreateViewAccessor())
        {
            byte* ptr = (byte*)view.SafeMemoryMappedViewHandle.DangerousGetHandle();
            WindowsNative.SecureZeroMemory(ptr, view.Capacity);
        }
    }
}