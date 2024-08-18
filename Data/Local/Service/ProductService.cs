using System.Diagnostics.CodeAnalysis;
using UdpProductServer.Data.Model;

namespace UdpProductServer.Data.Local.Service;
public class ProductService(DataContext data)
{
    private readonly DataContext data = data;
    public void Add([DisallowNull] Product newProduct)
    {
        this.data.Products.Add(newProduct);
        this.data.SaveChanges();
    }
    public List<Product> GetAll() => [.. this.data.Products];

    public Product? Find(uint id) => this.data.Products.FirstOrDefault(p => p.Id == id);
    public Product? Find(string name) => this.data.Products.FirstOrDefault(p => p.Name == name);
    public bool RemoveFromDb([DisallowNull] Product product)
    {
        this.data.Products.Remove(product);
        this.data.SaveChanges();
        return this.Find(product.Id) is null;
    }
}
