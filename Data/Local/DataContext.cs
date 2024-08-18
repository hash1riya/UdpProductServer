using Microsoft.EntityFrameworkCore;
using UdpProductServer.Data.Model;

namespace UdpProductServer.Data.Local;
public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
