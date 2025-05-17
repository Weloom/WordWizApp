namespace WordWiz.Tests.Mocks {
    public class TestFileReader : IFileReader {
        private readonly Dictionary<string, List<string>> _fileContent;

        public TestFileReader(Dictionary<string, List<string>> fileContent) => _fileContent = fileContent;

        public IEnumerable<string>? ReadLines(string relativePathAndfileName = "") {
            if(_fileContent.Any(x => x.Key == relativePathAndfileName)) {
                return _fileContent[relativePathAndfileName];
            }

            return null;
        }

        public IEnumerable<string>? GetFileNames(string relativePath = "") {
            return _fileContent.Select(_fileContent => _fileContent.Key).ToList();
        }
    }
}