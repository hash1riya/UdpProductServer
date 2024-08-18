using UdpProductServer.ViewModel.ProductPageViewModel;

namespace UdpProductServer.View;
public partial class ProductPage : ContentPage
{
	public ProductPage(ProductPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}