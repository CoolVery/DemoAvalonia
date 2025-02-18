using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Demo.ViewModels;

namespace Demo.Views;

public partial class ListPartners : UserControl
{
    public ListPartners()
    {
        InitializeComponent();
        DataContext = new ListPartnersViewModel();
    }
}