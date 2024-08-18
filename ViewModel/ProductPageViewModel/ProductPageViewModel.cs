using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UdpProductServer.Data.Local.Service;
using UdpProductServer.Data.Model;

namespace UdpProductServer.ViewModel.ProductPageViewModel;
public partial class ProductPageViewModel : ObservableObject
{
    private readonly ProductService products;
    [ObservableProperty]
    private Product newProduct = new();
    public ObservableCollection<Product> Products { get; set; } = [];

    public ProductPageViewModel(ProductService products)
    {
        this.products = products;
        this.Products = [.. this.products.GetAll()];
    }

    [RelayCommand]
    private void AddProduct()
    {
        this.products.Add(this.NewProduct);
        this.UpdateProductList();
        this.NewProduct = new();
    }
    [RelayCommand]
    private void RemoveProduct(uint id)
    {
        this.products.RemoveFromDb(this.products.Find(id));
        this.Products.Remove(this.Products.First(p => p.Id == id));
    }
    private void UpdateProductList()
    {
        List<Product> newState = this.products.GetAll();
        this.Products.Clear();
        foreach(var product in newState)
            this.Products.Add(product);
    }
}