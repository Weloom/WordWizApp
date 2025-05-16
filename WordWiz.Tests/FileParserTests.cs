using Moq;

namespace WordWiz.Tests {
    [TestClass]
    public sealed class FileParserTests {
        [TestMethod]
        public void ParseFile_CreateActionForFile_IsCalled() {
            var action = new Mock<IWordWizAction>();
            action.Setup(x => x.CreateActionForFile()).Verifiable();
            var fileReader = new Mock<IFileReader>();

            var sut = new FileParser(fileReader.Object);
            sut.ParseFile("file", action.Object);

            action.Verify(x => x.CreateActionForFile(), Times.Once);
        }
    }
}
