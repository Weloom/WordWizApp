public class WordCountFileAction : ILineAction {
    private Dictionary<string, int> _wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

    public Dictionary<string, int> WordCounts { get => _wordCounts; }

    public void Execute(string line) {
        string[] words = line.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach(string word in words) {
            string cleanedWord = word.Trim([',', '.', ':', ';', '?', '!']).ToLower();
            if(!string.IsNullOrEmpty(cleanedWord)) {
                _wordCounts[cleanedWord] = _wordCounts.TryGetValue(cleanedWord, out int count) ? count + 1 : 1;
            }
        }
    }
}
