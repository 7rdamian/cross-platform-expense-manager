using Project2MIP.ViewModels;

namespace Project2MIP;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Legăm ViewModel-ul la această pagină
        BindingContext = new MainViewModel();
    }
}
