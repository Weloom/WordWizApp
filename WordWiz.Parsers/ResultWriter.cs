public class ResultWriter : IResultWriter {
    private readonly string _rootPath;

    public ResultWriter(string rootPath) => _rootPath = rootPath;

    public void WriteCSVResults(Dictionary<string, int> keyValuePairs, string relativePathAndfileName) {
        string fullFileName = Path.Join(_rootPath, relativePathAndfileName);

        //check if directory exists and create it if it doesnt
        string directoryPath = Path.GetDirectoryName(fullFileName);
        if(!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath)) {
            Directory.CreateDirectory(directoryPath);
        }

        using(StreamWriter writer = new StreamWriter(fullFileName)) {
            foreach(var kvp in keyValuePairs.OrderBy(kvp => kvp.Key)) {
                writer.WriteLine($"{kvp.Key},{kvp.Value}");
            }
        }
    }

    public void WriteLineResults(List<string> lines, string relativePathAndfileName) {
        string fullFileName = Path.Join(_rootPath, relativePathAndfileName);
        using(StreamWriter writer = new StreamWriter(fullFileName)) {
            foreach(var line in lines) {
                writer.WriteLine(line);
            }
        }
    }
}
