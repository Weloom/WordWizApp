/// <summary>
/// Used by actions that must perform action on a single file, enumerating every text line. 
/// This class is used in combination with an action class supporting the <see cref="IWordWizAction"/>interface.
/// These actions can be passed to the <see cref="WordWiz.Parsers.Services.WordWizard"/>
/// </summary>
public interface ILineAction {
    /// <summary>
    /// Executes a custom action on the parsed text line
    /// </summary>
    void Execute(string line);
}

