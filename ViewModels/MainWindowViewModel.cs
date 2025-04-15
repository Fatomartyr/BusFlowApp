namespace BusFlowApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public BusFlowViewModel BusFlowViewModel { get; }

    public MainWindowViewModel()
    {
        BusFlowViewModel = new BusFlowViewModel();
    }
}