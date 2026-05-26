using Microsoft.Extensions.DependencyInjection;

namespace CityExplorer;

public partial class App : Application
{
    private readonly Views.MainTabbedPage _mainPage;

	public App(Views.MainTabbedPage mainPage)
	{
		InitializeComponent();
        _mainPage = mainPage;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(_mainPage);
	}
}