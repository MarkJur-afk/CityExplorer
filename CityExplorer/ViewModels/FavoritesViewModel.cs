using System.Collections.ObjectModel;
using System.Windows.Input;
using CityExplorer.Models;
using CityExplorer.Services;

namespace CityExplorer.ViewModels;

public class FavoritesViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    public ObservableCollection<FavoriteAttraction> Favorites { get; set; } = new();

    public ICommand RemoveFavoriteCommand { get; }
    public ICommand RefreshCommand { get; }

    public FavoritesViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        Title = "Избранное";

        RemoveFavoriteCommand = new Command<FavoriteAttraction>(async (f) => await RemoveFavorite(f));
        RefreshCommand = new Command(async () => await LoadFavorites());
    }

    private bool _isLoading;

    public async Task LoadFavorites()
    {
        if (_isLoading) return;
        _isLoading = true;

        try
        {
            IsBusy = true;
            Favorites.Clear();
            var items = await _databaseService.GetAllFavoritesAsync();
            foreach (var item in items)
            {
                Favorites.Add(item);
            }
        }
        finally
        {
            IsBusy = false;
            _isLoading = false;
        }
    }

    private async Task RemoveFavorite(FavoriteAttraction favorite)
    {
        if (favorite == null) return;

        await _databaseService.RemoveFavoriteAsync(favorite);
        Favorites.Remove(favorite);
    }
}
