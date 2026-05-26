using System.Collections.ObjectModel;
using System.Windows.Input;
using CityExplorer.Models;
using CityExplorer.Services;

namespace CityExplorer.ViewModels;

public class ExploreViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;
    private string _selectedCategory = "Всё";
    private Attraction? _selectedAttraction;

    public ObservableCollection<Attraction> AllAttractions { get; set; } = new();
    public ObservableCollection<Attraction> FilteredAttractions { get; set; } = new();

    public ICommand CategorySelectedCommand { get; }
    public ICommand AddToFavoritesCommand { get; }

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory != value)
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterAttractions();
            }
        }
    }

    public Attraction? SelectedAttraction
    {
        get => _selectedAttraction;
        set
        {
            if (_selectedAttraction != value)
            {
                _selectedAttraction = value;
                OnPropertyChanged();
            }
        }
    }

    public ExploreViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        Title = "Обзор";

        CategorySelectedCommand = new Command<string>(cat => SelectedCategory = cat);
        AddToFavoritesCommand = new Command<Attraction>(async (attraction) => await AddToFavorites(attraction));

        LoadData();
        FilterAttractions();
    }

    private void LoadData()
    {
        AllAttractions.Add(new Attraction { Id = 1, Name = "Старый город", Description = "Исторический центр Таллина", Category = "🏰", ImageUrl = "old_town.jpg" });
        AllAttractions.Add(new Attraction { Id = 2, Name = "Кадриорг", Description = "Великолепный парк и дворец", Category = "🌳", ImageUrl = "kadriorg.jpg" });
        AllAttractions.Add(new Attraction { Id = 3, Name = "Ратушная площадь", Description = "Сердце старого города", Category = "🏰", ImageUrl = "town_hall.jpg" });
        AllAttractions.Add(new Attraction { Id = 4, Name = "Телебашня", Description = "Самое высокое здание Эстонии", Category = "🗼", ImageUrl = "tv_tower.jpg" });
        AllAttractions.Add(new Attraction { Id = 5, Name = "Летная гавань", Description = "Морской музей", Category = "🚢", ImageUrl = "seaplane_harbour.jpg" });
    }

    private void FilterAttractions()
    {
        FilteredAttractions.Clear();
        foreach (var a in AllAttractions)
        {
            if (SelectedCategory == "Всё" || a.Category == SelectedCategory)
            {
                FilteredAttractions.Add(a);
            }
        }
    }

    private async Task AddToFavorites(Attraction attraction)
    {
        if (attraction == null) return;

        var existing = await _databaseService.GetFavoriteByAttractionIdAsync(attraction.Id);
        if (existing != null)
        {
            if (Application.Current?.Windows.Count > 0)
            {
                await Application.Current.Windows[0].Page!.DisplayAlert("Избранное", $"{attraction.Name} уже есть в избранном!", "ОК");
            }
            return;
        }

        var fav = new FavoriteAttraction
        {
            AttractionId = attraction.Id,
            Name = attraction.Name,
            Description = attraction.Description,
            Category = attraction.Category,
            ImageUrl = attraction.ImageUrl
        };

        await _databaseService.AddFavoriteAsync(fav);
        if (Application.Current?.Windows.Count > 0)
        {
            await Application.Current.Windows[0].Page!.DisplayAlert("Успех", $"{attraction.Name} добавлено в избранное!", "ОК");
        }
    }
}
