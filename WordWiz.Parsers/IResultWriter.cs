public interface IResultWriter {
    void WriteCSVResults(Dictionary<string, int> keyValuePairs, string filePath);

    void WriteLineResults(List<string> lines, string filePath);
}
