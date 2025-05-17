using WordWiz.Tests.Mocks;

namespace WordWiz.Components.Tests {
    [TestClass]
    public sealed class WordCountActionTests {
        [TestMethod]
        public void ParseFile_ExcludeList_WordExcluded() {
            var files = new Dictionary<string, List<string>> {
                { "file1", new List<string> {
                    "apple orange"
                    }
                },
                { @"\settings\excludelist.txt", new List<string> {
                    "orange"
                    }
                }
            };

            var fileReader = new TestFileReader(files);
            var resultWriter = new TestResultWriter();
            var wordCountAction = new WordCountAction(resultWriter, fileReader);

            new FileParser(fileReader).ParseFile("file1", wordCountAction);

            wordCountAction.OperationEnd();

            Assert.AreEqual(3, resultWriter.Results.Count);
            Assert.AreEqual("apple", resultWriter.Results["FILE_A.txt"].Trim());
            Assert.IsFalse(resultWriter.Results.Any(x => x.Key == "FILE_O.txt"));
            Assert.AreEqual("orange,1", resultWriter.Results["excludedwordscount.csv"].Trim());
            Assert.AreEqual("apple,1", resultWriter.Results["wordcount.csv"].Trim());
        }

        [TestMethod]
        public void ParseFile_NoExcludeList_NoWordExcluded() {
            var files = new Dictionary<string, List<string>> {
                { "file1", new List<string> {
                    "apple orange"
                    }
                }
            };

            var fileReader = new TestFileReader(files);
            var resultWriter = new TestResultWriter();
            var wordCountAction = new WordCountAction(resultWriter, fileReader);

            new FileParser(fileReader).ParseFile("file1", wordCountAction);

            wordCountAction.OperationEnd();

            Assert.AreEqual(4, resultWriter.Results.Count);
            Assert.AreEqual("apple", resultWriter.Results["FILE_A.txt"].Trim());
            Assert.AreEqual("orange", resultWriter.Results["FILE_O.txt"].Trim());
            Assert.AreEqual("", resultWriter.Results["excludedwordscount.csv"].Trim());
            Assert.AreEqual("apple,1\r\norange,1", resultWriter.Results["wordcount.csv"].Trim());
        }

        [TestMethod]
        public void ParseFile_MultipleFiles_WordCountIsCombined() {

            var files = new Dictionary<string, List<string>> {
                { "file1", new List<string> {
                    "apple orange"
                    }
                },
                   { "file2", new List<string> {
                    "apple orange"
                    }
                }
            };

            var fileReader = new TestFileReader(files);
            var resultWriter = new TestResultWriter();
            var wordCountAction = new WordCountAction(resultWriter, fileReader);

            new FileParser(fileReader).ParseFile("file1", wordCountAction);
            new FileParser(fileReader).ParseFile("file2", wordCountAction);

            wordCountAction.OperationEnd();

            Assert.AreEqual(4, resultWriter.Results.Count);
            Assert.AreEqual("apple", resultWriter.Results["FILE_A.txt"].Trim());
            Assert.AreEqual("orange", resultWriter.Results["FILE_O.txt"].Trim());
            Assert.AreEqual("", resultWriter.Results["excludedwordscount.csv"].Trim());
            Assert.AreEqual("apple,2\r\norange,2", resultWriter.Results["wordcount.csv"].Trim());
        }

        [TestMethod]
        [Ignore]
        public void ParseFile_SoManyMoreTests() {
            //...
        }
    }
}
