/// <summary>
/// Implements features for writing results to a csv file or txt file 
/// </summary>
public class ResultWriter : IResultWriter {
    private readonly string _rootPath;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="rootPath">A root path that is joined with the file specific path e.g. "c:\myapp\results\"</param>
    public ResultWriter(string rootPath) => _rootPath = rootPath;

    public void WriteDictionaryToCsvFile(Dictionary<string, int> dictionary, string relativePathAndFileName) {
        string fullFileName = Path.Join(_rootPath, relativePathAndFileName);

        //check if directory exists and create it if it doesn't
        string directoryPath = Path.GetDirectoryName(fullFileName) ?? "";
        if(!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath)) {
            Directory.CreateDirectory(directoryPath);
        }

        using(StreamWriter writer = new StreamWriter(fullFileName)) {
            foreach(var entry in dictionary.OrderBy(entry => entry.Key)) {
                writer.WriteLine($"{entry.Key},{entry.Value}");
            }
        }
    }

    public void WriteListToTextFile(List<string> lines, string relativePathAndFileName) {
        string fullFileName = Path.Join(_rootPath, relativePathAndFileName);
        using(StreamWriter writer = new StreamWriter(fullFileName)) {
            foreach(var line in lines) {
                writer.WriteLine(line);
            }
        }
    }
}
