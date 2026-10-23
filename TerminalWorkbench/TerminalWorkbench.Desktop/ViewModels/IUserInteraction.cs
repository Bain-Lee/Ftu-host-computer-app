namespace TerminalWorkbench.Desktop.ViewModels
{
    public interface IUserInteraction
    {
        string SelectConfiguration(string runtimeDirectory);
        void OpenOriginalTool(string runtimeDirectory);
    }
}
