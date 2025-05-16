/// <summary>
/// Used by the <see cref="WordWiz.Parsers.Services.WordWiz"/> and handles access to an individual file and instantiates a line parser for each lin ein the file.
/// Actual file access is handeld by the fileReader, so the 'file' can be of any datasource type.
/// </summary>
public class FileParser {
    private readonly IFileReader _fileReader;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileReader">Abstracts file access so the 'file' can be of any datasource type.</param>
    public FileParser(IFileReader fileReader) => _fileReader = fileReader;

    /// <summary>
    /// Executes an action on the fiule.
    /// </summary>
    /// <param name="fullfilePath">The full filepath of the file e.g. "c:\...\text1.txt"</param>
    /// <param name="action">Action used to perform operation on the file content</param>
    public void ParseFile(string fullfilePath, IWordWizAction action) {
        var fileAction = action.CreateActionForFile();
        foreach(string line in _fileReader.ReadLines(fullfilePath)) {
            new LineParser().ParseLine(line, fileAction);
        }
    }
}
