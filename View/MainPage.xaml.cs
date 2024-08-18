using UdpProductServer.ViewModel.MainViewModel;

namespace UdpProductServer.View;
public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}
