using Avalonia.Controls;
using Demo.Models;
using Demo.Views;
using ReactiveUI;

namespace Demo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl currentUC;
        DemoContext context;

        public static MainWindowViewModel Inctance;

        public UserControl CurrentUC { get => currentUC; set => this.RaiseAndSetIfChanged(ref currentUC, value); }
        public DemoContext Context { get => context; set => context = value; }

        public MainWindowViewModel()
        {
            Inctance = this;
            context = new DemoContext();
            currentUC = new ListPartners();
        }
    }
}
