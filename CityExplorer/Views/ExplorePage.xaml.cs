using CityExplorer.ViewModels;

namespace CityExplorer.Views;

public partial class ExplorePage : ContentPage
{
    public ExplorePage(ExploreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
