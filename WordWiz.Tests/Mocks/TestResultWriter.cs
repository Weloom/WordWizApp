using System.Text;

namespace WordWiz.Tests.Mocks {
    public class TestResultWriter : IResultWriter {
        public Dictionary<string, string> Results { get; } = new Dictionary<string, string>();

        public void WriteDictionarytoCsvFile(Dictionary<string, int> dictionary, string filePath) {
            var sb = new StringBuilder();
            foreach(var entry in dictionary.OrderBy(entry => entry.Key)) {
                sb.AppendLine($"{entry.Key},{entry.Value}");
            }

            Results[filePath] = sb.ToString();
        }

        public void WriteListToTextFile(List<string> lines, string filePath) {
            var sb = new StringBuilder();
            foreach(var line in lines) {
                sb.AppendLine(line);
            }

            Results[filePath] = sb.ToString();
        }
    }
}
