/// <summary>
/// Used by the <see cref="WordWiz.Parsers.Services.WordWiz"/> and handles access to an individual file and instantiates a line parser for each lin ein the file.
/// Actual file access is handled by the fileReader, so the 'file' can be of any data source type.
/// </summary>
public class FileParser {
    private readonly IFileReader _fileReader;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileReader">Abstracts file access so the 'file' can be of any data source type.</param>
    public FileParser(IFileReader fileReader) => _fileReader = fileReader;

    /// <summary>
    /// Executes an action on the file.
    /// </summary>
    /// <param name="fullfilePath">The full file path of the file e.g. "c:\...\text1.txt"</param>
    /// <param name="action">Action used to perform operation on the file content</param>
    public void ParseFile(string fullFilePath, IWordWizAction action) {
        var fileAction = action.CreateActionForFile();
        var lines = _fileReader.ReadLines(fullFilePath);
        if(lines == null) {
            throw new FileNotFoundException($"File not found: {fullFilePath}");
        }

        foreach(string line in lines) {
            new LineParser().ParseLine(line, fileAction);
        }
    }
}
