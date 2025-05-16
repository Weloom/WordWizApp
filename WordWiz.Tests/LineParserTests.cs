using Moq;

namespace WordWiz.Tests {
    [TestClass]
    public sealed class LineParserTests {
        [TestMethod]
        public void ParseLine_Execute_IsCalled() {
            var lineAction = new Mock<ILineAction>();
            lineAction.Setup(x => x.Execute(It.IsAny<string>())).Verifiable();

            LineParser lineParser = new LineParser();
            lineParser.ParseLine("testline", lineAction.Object);

            lineAction.Verify(x => x.Execute("testline"), Times.Once);
        }
    }
}
