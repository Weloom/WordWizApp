using CommandLine;

namespace WordWiz {
    public class Program {
        public static void Main(string[] args) {
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
    }
}

/*
 TODO:
 * break action into two
 
 * Create all tests
 
 * nullable

 * handle if files are empty, list of warning - log file
 * handle if excludelist doesnt exist * run real app
 * Error handling
 */