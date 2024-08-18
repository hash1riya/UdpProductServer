using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UdpProductServer.Data.Local;
using UdpProductServer.Data.Local.Service;
using UdpProductServer.View;
using UdpProductServer.ViewModel.MainViewModel;
using UdpProductServer.ViewModel.ProductPageViewModel;

namespace UdpProductServer;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        string dbName = "data.db";
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);

        builder.Services
            .AddDbContext<DataContext>(options => options.UseSqlite($"Data Source={dbPath}"))
            .AddSingleton<MainPage>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<ProductPage>()
            .AddSingleton<ProductPageViewModel>()
            .AddSingleton<ProductService>()
            .AddSingleton<AppShell>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            DataContext temp = app.Services.GetRequiredService<DataContext>();
            temp.Database.EnsureCreated();
        }

        return app;
    }
}
