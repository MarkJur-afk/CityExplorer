using Microsoft.Extensions.Logging;
using CityExplorer.Services;
using CityExplorer.ViewModels;
using CityExplorer.Views;

namespace CityExplorer;

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

        // Services
        builder.Services.AddSingleton<DatabaseService>();

        // ViewModels
        builder.Services.AddTransient<ExploreViewModel>();
        builder.Services.AddTransient<FavoritesViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();

        // Views
        builder.Services.AddTransient<ExplorePage>();
        builder.Services.AddTransient<FavoritesPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<MainTabbedPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

