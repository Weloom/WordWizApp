public class LineParser {
    public void ParseLine(string line, ILineAction lineAction) {
        lineAction.Execute(line);
    }
}
