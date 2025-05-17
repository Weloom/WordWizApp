using CommandLine;

namespace WordWiz {
    public class Program {
        public static void Main(string[] args) {
            try {
                Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => {
                    Console.WriteLine($"Performs operations on files in '{o.SourceDirectory}'. Updates '{o.TargetDirectory}'");

                    var rootDir = AppDomain.CurrentDomain.BaseDirectory;
                    string fullSourceDirectory = Path.GetFullPath(o.SourceDirectory ?? "");
                    string fullTargetDirectory = Path.GetFullPath(o.TargetDirectory ?? "");

                    //List of actions contains currently only a single action, but could be extended with e.g. late binding (Factory pattern)g
                    var actions = new List<IWordWizAction> {
                        new WordCountAction(new ResultWriter(fullTargetDirectory), new FileReader(rootDir))
                    };

                    new Parsers.Services.WordWizard(new FileReader(fullSourceDirectory), actions).ParseFiles();

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
