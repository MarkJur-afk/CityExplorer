using System.ComponentModel;
using System.Runtime.CompilerServices;
using CityExplorer.Resources.Strings;

namespace CityExplorer.Models;

public class Attraction : INotifyPropertyChanged
{
    public Attraction()
    {
        LocalizationResourceManager.Instance.PropertyChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Description));
        };
    }

    public int Id { get; set; }
    
    public string NameKey { get; set; }
    public string Name => LocalizationResourceManager.Instance[NameKey];

    public string DescriptionKey { get; set; }
    public string Description => LocalizationResourceManager.Instance[DescriptionKey];

    public string ImageUrl { get; set; }
    public string Category { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
