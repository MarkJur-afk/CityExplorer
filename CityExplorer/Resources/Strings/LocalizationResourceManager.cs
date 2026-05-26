using System.ComponentModel;
using System.Globalization;

namespace CityExplorer.Resources.Strings;

public class LocalizationResourceManager : INotifyPropertyChanged
{
    private LocalizationResourceManager()
    {
        AppResources.Culture = CultureInfo.CurrentUICulture;
    }

    public static LocalizationResourceManager Instance { get; } = new();

    public string this[string text] => AppResources.ResourceManager.GetString(text, AppResources.Culture) ?? text;

    public void SetCulture(CultureInfo culture)
    {
        AppResources.Culture = culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
