public class FileReader : IFileReader {
    private readonly string _rootPath;

    public FileReader(string rootPath) => _rootPath = rootPath;

    public IEnumerable<string> ReadLines(string relativePathAndfileName = "") {
        string fullFileName = Path.Join(_rootPath, relativePathAndfileName);

        return File.ReadLines(fullFileName);
    }

    public IEnumerable<string> GetFileNames(string relativePath = "") {
        string fullPath = Path.Join(_rootPath, relativePath);
        return Directory.EnumerateFiles(fullPath);
    }
}

