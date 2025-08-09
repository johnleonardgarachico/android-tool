namespace ChicoKoodo.AndroidApp.Interfaces.Platforms.Android
{
    public interface IFileHelper
    {
        Task SaveFileAsync(string fileName, string targetPath, string fileContent);
        Task<string?> ReadFileAsync(string fileName, string targetPath);
        Task<IEnumerable<string>> ReadFilesAsync(string targetPath);
    }
}
