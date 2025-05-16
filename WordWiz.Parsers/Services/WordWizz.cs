namespace WordWiz.Parsers.Services {
    public class WordWiz {
        private readonly List<IWordWizAction> _actions;
        private readonly IFileReader _fileReader;

        public WordWiz(IFileReader fileReader, List<IWordWizAction> actions) => (_fileReader, _actions) = (fileReader, actions);

        public void ParseFiles() {
            foreach(var action in _actions) {
                try {
                    // Process each file in parallel
                    var files = _fileReader.GetFileNames();
                    Parallel.ForEach(files, filePath => {
                        try {
                            var fileParser = new FileParser(_fileReader);
                            fileParser.ParseFile(Path.GetFileName(filePath), action); //YDRK!!!!!!!!!!
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
