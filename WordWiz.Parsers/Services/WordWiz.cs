namespace WordWiz.Parsers.Services {
    /// <summary>
    /// Service accepts a number of actions supporting the <see cref="IWordWizAction"/> and executes them against oen or more text files.
    /// </summary>
    public class WordWiz {
        private readonly List<IWordWizAction> _actions;
        private readonly IFileReader _fileReader;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileReader">Used to access the lists of files to parse.</param>
        /// <param name="actions">List of actions that are to be executed against the files found by the fileReader.</param>
        public WordWiz(IFileReader fileReader, List<IWordWizAction> actions) => (_fileReader, _actions) = (fileReader, actions);

        /// <summary>
        /// For each action enumerate all files found by the fileReader in parallel. 
        /// </summary>
        public void ParseFiles() {
            foreach(var action in _actions) {
                try {
                    // Process each file in parallel
                    var files = _fileReader.GetFileNames();
                    Parallel.ForEach(files, filePath => {
                        try {
                            var fileParser = new FileParser(_fileReader);
                            fileParser.ParseFile(Path.GetFileName(filePath), action); //TODO: Smell
                        }
                        catch(Exception ex) {
                            Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                        }
                    });

                    EndOperation();
                }

                catch(Exception ex) {
                    Console.WriteLine($"Error executing action '{action.GetType().FullName}': {ex.Message}");
                }
            }
        }

        private void EndOperation() {
            foreach(var action in _actions) {
                action.OperationEnd();
            }
        }
    }
}
