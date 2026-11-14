# CityExplorer

A small cross-platform guide to the sights of **Tallinn**, built with **.NET MAUI**. Browse attractions by category, switch the interface language on the fly, and keep a list of favourites that is saved on your device.

## Features

- **Explore** – a list of Tallinn attractions (Old Town, Kadriorg, Town Hall Square, TV Tower, Seaplane Harbour), each with a photo, a short description and a category icon.
- **Category filter** – show everything or only one category (🏰 🌳 🗼 🚢).
- **Favourites** – add an attraction to your favourites with one tap and remove it later. Favourites are stored in a local SQLite database, so they survive app restarts. Adding the same place twice is detected and politely refused.
- **Live language switching** – choose Русский, Eesti or English in Settings and the whole interface updates immediately, without restarting the app.

## Tech stack

| Area | Choice |
| --- | --- |
| Language | C# |
| Framework | .NET MAUI on .NET 10 |
| Pattern | MVVM (view models with `INotifyPropertyChanged`, commands, `ObservableCollection`) |
| Local storage | SQLite via `sqlite-net-pcl` |
| Localisation | `.resx` resource files and a custom `LocalizationResourceManager` that raises change notifications, so bindings refresh when the language changes |
| Targets | Android, iOS, Mac Catalyst, Windows |

## Project structure

```text
CityExplorer/
├── Models/            Attraction, FavoriteAttraction
├── Services/          DatabaseService (async SQLite access)
├── ViewModels/        BaseViewModel, ExploreViewModel, FavoritesViewModel, SettingsViewModel
├── Views/             MainTabbedPage, ExplorePage, FavoritesPage, SettingsPage
├── Resources/
│   └── Strings/       AppResources.resx / .en.resx / .ru.resx, LocalizationResourceManager
└── Platforms/         Platform-specific startup code
```

The app uses a tabbed layout with three tabs: **Explore**, **Favorites** and **Settings**. Pages are created through dependency injection, and `DatabaseService` creates the database table on first use.

## How the language switching works

Attraction names and descriptions are stored as resource keys (for example `Attraction1_Name`). The `Attraction` model reads the text through `LocalizationResourceManager.Instance[key]`. When the user picks another language, the manager sets the new culture and raises `PropertyChanged`, and XAML bindings of the form `{Binding [Key], Source={x:Static strings:LocalizationResourceManager.Instance}}` redraw the text.

## Getting started

### Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- The .NET MAUI workload:

  ```bash
  dotnet workload install maui
  ```

- Visual Studio 2022 or newer with the *.NET Multi-platform App UI development* workload, or Visual Studio Code with the .NET MAUI extension
- An Android emulator or device, an iOS simulator (macOS only), or Windows 10 or newer

### Run

```bash
git clone https://github.com/MarkJur-afk/CityExplorer.git
cd CityExplorer/CityExplorer

# pick one target
dotnet build -t:Run -f net10.0-android
dotnet build -t:Run -f net10.0-maccatalyst   # macOS
dotnet build -t:Run -f net10.0-windows10.0.19041.0   # Windows
```

The iOS and Mac Catalyst targets build only on macOS, and the Windows target only on Windows.

## Roadmap

- Add an Estonian translation (`AppResources.et.resx`); the language is already in the picker.
- Load attractions from a data source instead of the hard-coded list, and add more places and categories.
- Show attractions on a map.
- Cache images for offline use.

## Author

Made by [Mark](https://github.com/MarkJur-afk) while learning .NET MAUI.
