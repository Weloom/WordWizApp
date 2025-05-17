/// <summary>
/// Used to abstract file system access, so the source can be other than actual files and directories
/// </summary>
public interface IFileReader {
    IEnumerable<string>? ReadLines(string relativePathAndfileName = "");

    IEnumerable<string>? GetFileNames(string relativePath = "");
}
