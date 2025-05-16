/// <summary>
/// Used to abstract how to persist results.
/// </summary>
public interface IResultWriter {
    void WriteDictionarytoCsvFile(Dictionary<string, int> keyValuePairs, string filePath);

    void WriteListToTextFile(List<string> lines, string filePath);
}
