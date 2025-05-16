/// <summary>
/// Executes the <see cref="WordCountFileAction"/> on one or more files. 
/// Also collects all results and excludes any words that should be excluded.
/// </summary>
public class WordCountAction : IWordWizAction {
    private readonly List<ILineAction> _actions = new List<ILineAction>();
    private readonly IResultWriter _resultWriter;
    private List<string> _excludeList = new List<string>();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="resultWriter">Used to access text files</param>
    /// <param name="excludeListFileReader">Used to write results</param>
    public WordCountAction(IResultWriter resultWriter, IFileReader excludeListFileReader) {
        _resultWriter = resultWriter;

        //get list of excluded words
        var words = excludeListFileReader.ReadLines(@"\settings\excludelist.txt");
        if(words != null) {
            foreach(var word in words) {
                _excludeList.Add(word.Trim().ToLower());
            }
        }
    }

    public ILineAction CreateActionForFile() {
        var newFileAction = new WordCountFileAction();
        _actions.Add(newFileAction);
        return newFileAction;
    }

    /// <summary>
    /// Combines the count of words from all files and writes it to a csv file without excluded files (wordcount.csv).
    /// Writes collection of excluded words and the number of appearances to a csv file (excludedwordscount.csv)
    /// Writes all words and their total count to separate csv files based on their starting letter (E.g. "a.txt") 
    /// </summary>
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
        _resultWriter.WriteDictionaryToCsvFile(excludedWordCount, $"excludedwordscount.csv");

        // Persist the word count to a csv file
        _resultWriter.WriteDictionaryToCsvFile(filteredWordCount, $"wordcount.csv");

        // Select all the words that start with the same letter and save them to a coresponding file
        var groupedWords = filteredWordCount.GroupBy(wc => wc.Key[0]);
        foreach(var group in groupedWords) {
            string startingLetter = group.Key.ToString().ToUpper();
            _resultWriter.WriteListToTextFile(group.Select(group => group.Key).ToList(), $"FILE_{startingLetter}.txt");
        }
    }
}
