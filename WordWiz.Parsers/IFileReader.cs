public interface IFileReader {
    IEnumerable<string> ReadLines(string relativePathAndfileName = "");

    IEnumerable<string> GetFileNames(string relativePath = "");
}
