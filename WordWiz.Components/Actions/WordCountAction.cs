public class WordCountAction : IWordWizAction {
    private readonly List<ILineAction> _actions = new List<ILineAction>();
    private readonly IResultWriter _resultWriter;
    private List<string> _excludeList = new List<string>();

    public WordCountAction(IResultWriter resultWriter, IFileReader excludeListFileReader) {
        _resultWriter = resultWriter;

        //get list of excluded words
        foreach(var word in excludeListFileReader.ReadLines(@"\settings\excludelist.txt")) {
            _excludeList.Add(word.Trim().ToLower());
        }
    }

    public ILineAction CreateActionForFile() {
        var newFileAction = new WordCountFileAction();
        _actions.Add(newFileAction);
        return newFileAction;
    }

    public void OperationEnd() {
        //combine word counts from all files
        var wordCountsForAllFiles = new Dictionary<string, int>();
        foreach(var action in _actions) {
            foreach(var wc in ((WordCountFileAction)action).WordCounts) {
                wordCountsForAllFiles[wc.Key] = wordCountsForAllFiles.TryGetValue(wc.Key, out int count) ? count + wc.Value : wc.Value;
            }
        }

        //sort words by letter
        wordCountsForAllFiles = wordCountsForAllFiles.OrderBy(wc => wc.Key).ToDictionary(wc => wc.Key, wc => wc.Value);

        // Filter out excluded words
        var excludedWordCount = wordCountsForAllFiles.Where(wc => _excludeList.Contains(wc.Key)).ToDictionary();
        var filteredWordCount = wordCountsForAllFiles.Where(wc => !excludedWordCount.Contains(wc)).ToDictionary(wc => wc.Key, wc => wc.Value);

        // Persist excluded word count to a csv file
        _resultWriter.WriteCSVResults(excludedWordCount, $"excludedwordscount.csv");

        // Persist the word count to a csv file
        _resultWriter.WriteCSVResults(filteredWordCount, $"wordcount.csv");

        // Select all the words that start with the same letter and save them to a coresponding file
        var groupedWords = filteredWordCount.GroupBy(wc => wc.Key[0]);
        foreach(var group in groupedWords) {
            string startingLetter = group.Key.ToString();
            _resultWriter.WriteLineResults(group.Select(group => group.Key).ToList(), $"{startingLetter}.txt");
        }
    }
}
