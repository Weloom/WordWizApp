public class FileReader : IFileReader {
    private readonly string _rootPath;

    public FileReader(string rootPath) => _rootPath = rootPath;

    public IEnumerable<string>? ReadLines(string relativePathAndFileName = "") {
        string fullFileName = Path.Join(_rootPath, relativePathAndFileName);
        if(File.Exists(fullFileName)) {
            return File.ReadLines(fullFileName);
        }

        return null;
    }

    public IEnumerable<string>? GetFileNames(string relativePath = "") {
        string fullPath = Path.Join(_rootPath, relativePath);
        if(Directory.Exists(fullPath)) {
            return Directory.EnumerateFiles(fullPath);
        }

        return null;
    }
}

