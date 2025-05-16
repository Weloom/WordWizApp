public interface IWordWizAction {
    ILineAction CreateActionForFile();

    void OperationEnd();
}