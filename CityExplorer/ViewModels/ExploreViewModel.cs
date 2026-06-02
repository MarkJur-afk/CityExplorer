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
        AllAttractions.Add(new Attraction { Id = 1, NameKey = "Attraction1_Name", DescriptionKey = "Attraction1_Desc", Category = "🏰", ImageUrl = "https://7d9e88a8-f178-4098-bea5-48d960920605.selcdn.net/8977788c-5eb8-4fd0-88a6-b54c57e83257/" });
        AllAttractions.Add(new Attraction { Id = 2, NameKey = "Attraction2_Name", DescriptionKey = "Attraction2_Desc", Category = "🌳", ImageUrl = "https://live.s3.teliahybridcloud.com/s3fs-public/styles/honorary_thumbnail_autoplay_gif/public/image_bank_foto/2022-06/275785.jpg?itok=Oip2ouLE" });
        AllAttractions.Add(new Attraction { Id = 3, NameKey = "Attraction3_Name", DescriptionKey = "Attraction3_Desc", Category = "🏰", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/18/Tallinna_Raekoda_11-06-2013.jpg" });
        AllAttractions.Add(new Attraction { Id = 4, NameKey = "Attraction4_Name", DescriptionKey = "Attraction4_Desc", Category = "🗼", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRw3YU27zP1jDs_Bg1lbFsV_J5WWOQHJPQriQ&s" });
        AllAttractions.Add(new Attraction { Id = 5, NameKey = "Attraction5_Name", DescriptionKey = "Attraction5_Desc", Category = "🚢", ImageUrl = "https://pitbus.ru/wp-content/uploads/2019/06/letnyaya-gavan-1024x575.jpg" });
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
