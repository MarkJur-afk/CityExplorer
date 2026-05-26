using CityExplorer.Resources.Strings;

namespace CityExplorer.Views;

public partial class MainTabbedPage : TabbedPage
{
    public MainTabbedPage(ExplorePage explorePage, FavoritesPage favoritesPage, SettingsPage settingsPage)
    {
        InitializeComponent();
        
        explorePage.SetBinding(Page.TitleProperty, new Binding("[ExploreTitle]", source: LocalizationResourceManager.Instance));
        favoritesPage.SetBinding(Page.TitleProperty, new Binding("[FavoritesTitle]", source: LocalizationResourceManager.Instance));
        settingsPage.SetBinding(Page.TitleProperty, new Binding("[SettingsTitle]", source: LocalizationResourceManager.Instance));

        Children.Add(explorePage);
        Children.Add(favoritesPage);
        Children.Add(settingsPage);
    }
}
