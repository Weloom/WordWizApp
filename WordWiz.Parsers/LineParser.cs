/// <summary>
/// Helps a line action perform operations on a text line
/// </summary>
public class LineParser {
    /// <summary>
    /// Helps a line action perform operations on a text line
    /// </summary>
    /// <param name="line">Any text string</param>
    /// <param name="lineAction">A line action that can perform actions on the line input</param>
    public void ParseLine(string line, ILineAction lineAction) {
        lineAction.Execute(line);
    }
}
