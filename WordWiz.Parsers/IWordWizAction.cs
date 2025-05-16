/// <summary>
/// Used by actions that must perform action on one or more files, controlled by the <see cref="WordWiz.Parsers.Services.WordWiz"/>
/// </summary>
public interface IWordWizAction {
    /// <summary>
    /// Any action supporting the <see cref="IWordWizAction"/> should have a file specific action using the <see cref="ILineAction"/> 
    /// that performs actions on the individual files. This method should support instantiating an instance of this class for each file to parse.
    /// </summary>
    ILineAction CreateActionForFile();

    /// <summary>
    /// Use for code performed after files have been parsed. For thread unsafe code.
    /// </summary>
    void OperationEnd();
}