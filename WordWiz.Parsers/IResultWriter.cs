/// <summary>
/// Used to abstract how to persist results.
/// </summary>
public interface IResultWriter {
    void WriteDictionaryToCsvFile(Dictionary<string, int> keyValuePairs, string filePath);

    void WriteListToTextFile(List<string> lines, string filePath);
}
