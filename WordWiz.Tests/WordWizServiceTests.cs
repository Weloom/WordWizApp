using Moq;

namespace WordWiz.Tests {
    [TestClass]
    public sealed class WordWizServiceTests {
        [TestMethod]
        public void ParseFiles() {
            //mock filereader
            var fileReader = new Mock<IFileReader>();
            var files = new Dictionary<string, List<string>> {
                { "file1", new List<string>() }
            };

            fileReader.Setup(x => x.GetFileNames(It.IsAny<string>())).Returns(files.Keys.ToList());
            fileReader.Setup(x => x.ReadLines(It.IsAny<string>())).Returns((string fileName) => {
                if(files.ContainsKey(fileName)) {
                    return files[fileName];
                }

                return new List<string>();
            });

            //mock action
            var actions = new List<IWordWizAction>();
            var action = new Mock<IWordWizAction>();
            action.Setup(x => x.CreateActionForFile()).Returns(new Mock<ILineAction>().Object);
            actions.Add(action.Object);

            var sut = new WordWiz.Parsers.Services.WordWizard(fileReader.Object, actions);
            sut.ParseFiles();

            fileReader.Verify(x => x.GetFileNames(It.IsAny<string>()), Times.Once);
            action.Verify(x => x.CreateActionForFile(), Times.Once);
            action.Verify(x => x.OperationEnd(), Times.Once);
        }
    }
}
