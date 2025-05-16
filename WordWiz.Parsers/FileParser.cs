public class FileParser {
    private readonly IFileReader _fileReader;

    public FileParser(IFileReader fileReader) => _fileReader = fileReader;

    public void ParseFile(string filePath, IWordWizAction action) {
        var fileAction = action.CreateActionForFile();
        foreach(string line in _fileReader.ReadLines(filePath)) {
            new LineParser().ParseLine(line, fileAction);
        }
    }
}
