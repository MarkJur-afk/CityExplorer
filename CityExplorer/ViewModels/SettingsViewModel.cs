using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace CityExplorer.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private string _selectedLanguage = string.Empty;

    public ObservableCollection<string> Languages { get; set; } = new()
    {
        "Русский",
        "Eesti",
        "English"
    };

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (_selectedLanguage != value)
            {
                _selectedLanguage = value;
                OnPropertyChanged();
                ChangeLanguage(_selectedLanguage);
            }
        }
    }

    public SettingsViewModel()
    {
        Title = "Настройки";
        var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        if (culture == "ru") SelectedLanguage = "Русский";
        else if (culture == "et") SelectedLanguage = "Eesti";
        else SelectedLanguage = "English";
    }

    private void ChangeLanguage(string language)
    {
        string cultureCode = "en";
        if (language == "Русский") cultureCode = "ru";
        else if (language == "Eesti") cultureCode = "et";

        var culture = new CultureInfo(cultureCode);
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        CultureInfo.CurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;

        // Обновляем ресурсы через менеджер, что автоматически обновит весь интерфейс
        CityExplorer.Resources.Strings.LocalizationResourceManager.Instance.SetCulture(culture);
    }
}
