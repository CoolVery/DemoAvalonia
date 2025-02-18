using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Demo.ViewModels;

namespace Demo.Views;

public partial class NewPartner : UserControl
{
    public NewPartner()
    {
        InitializeComponent();
        DataContext = new NewPartnerViewModel();
    }
    public void NumericUpDown_TextInput(object? sender, TextInputEventArgs e)
    {
        // Отменяем ввод (делаем вид, что ничего не произошло)
        e.Handled = true;
    }
}