using WordWiz.Tests.Mocks;

namespace WordWiz.Components.Tests {
    [TestClass]
    public sealed class WordCountTests {
        [TestMethod]
        public void TestMethod1() {

            var file1Lines = new List<string> {
                "apple banana apple",
                "orange"
            };

            var excludedWords = new List<string> {
                "banana"
            };

            var files = new Dictionary<string, List<string>> {
                { "file1", file1Lines },
                { @"\settings\excludelist.txt", excludedWords}
            };

            var fileReader = new TestFileReader(files);
            var resultWriter = new TestResultWriter();
            var wordCountAction = new WordCountAction(resultWriter, fileReader);

            new FileParser(fileReader).ParseFile("file1", wordCountAction);

            wordCountAction.OperationEnd();

            Assert.AreEqual(4, resultWriter.Results.Count);
        }

        [TestMethod]
        public void TestMethod2() {

            var file1Lines = new List<string> {
                "apple banana apple",
                "orange"
            };

            var excludedWords = new List<string> {
                "banana"
            };

            var files = new Dictionary<string, List<string>> {
                { "file1", file1Lines },
                { @"\settings\excludelist.txt", excludedWords}
            };

            var WordWizServiceFiles = new Dictionary<string, List<string>> {
                { "file1", file1Lines },
            };

            var fileReader = new TestFileReader(files);
            var WordWizServicefileReader = new TestFileReader(WordWizServiceFiles);
            var resultWriter = new TestResultWriter();
            var wordCountAction = new WordCountAction(resultWriter, fileReader);

            new WordWiz.Parsers.Services.WordWiz(fileReader, new List<IWordWizAction>() { wordCountAction }).ParseFiles();

            Assert.AreEqual(4, resultWriter.Results.Count);
        }
    }
}
