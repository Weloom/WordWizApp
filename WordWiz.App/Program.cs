using CommandLine;

namespace WordWiz {
    public class Program {
        public static void Main(string[] args) {
            try {
                Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => {
                    Console.WriteLine($"Performs operations on files in '{o.SourceDictionary}'. Updates '{o.SourceDictionary}'");

                    string fullSourceDirectory = Path.GetFullPath(o.SourceDictionary ?? "");
                    string fullTargetDirectory = Path.GetFullPath(o.TargetDictionary ?? "");
                    var rootDir = AppDomain.CurrentDomain.BaseDirectory;

                    //List of actions contains currently only a single action, but could be extended with e.g. late binding (Factory pattern)g
                    var actions = new List<IWordWizAction> {
                    new WordCountAction(new ResultWriter(fullTargetDirectory), new FileReader(rootDir))
                };

                    new Parsers.Services.WordWiz(new FileReader(fullSourceDirectory), actions).ParseFiles();

                    Console.WriteLine("Operations done. Press any key to exit.");
                });
            }
            catch(Exception ex) {
                Console.WriteLine($"An exception occurred. Expects results to be invalid or incomplete. Exception: {ex.Message}");
                //logging goes here
            }
        }
    }
}
