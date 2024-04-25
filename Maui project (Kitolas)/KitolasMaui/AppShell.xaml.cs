using Kitolas.Model;
using Kitolas.Persistence;
using KitolasMaui.View;
using KitolasMaui.ViewModel;

namespace KitolasMaui;

public partial class AppShell : Shell
{
    private IKitolasDataAccess _kitolasDataAccess;
    private  Game _kitolasGameModel;
    private  KitolasViewModel _kitolasViewModel;

    private  IStore _store;
    private  StoredGameBrowserModel _storedGameBrowserModel;
    private  StoredGameBrowserViewModel _storedGameBrowserViewModel;

    public AppShell(IStore kitolasStore,
        IKitolasDataAccess kitolasDataAccess,
        Game kitolasGameModel,
        KitolasViewModel kitolasViewModel)
    {
		InitializeComponent();

        _store = kitolasStore;
        _kitolasDataAccess = kitolasDataAccess;
        _kitolasGameModel = kitolasGameModel;
        _kitolasViewModel = kitolasViewModel;


        _kitolasGameModel.gameEnd += KitolasGameModel_GameOver;

        _kitolasViewModel.NewGame += KitolasViewModel_NewGame;    
        _kitolasViewModel.LoadGame += KitolasViewModel_LoadGame;
        _kitolasViewModel.SaveGame += KitolasViewModel_SaveGame;
        _kitolasViewModel.ExitGame += KitolasViewModel_ExitGame;
        _kitolasViewModel.MapChange += KitolasViewModel_MapChange;
        _kitolasViewModel.Slide += KitolasViewModel_Slide;
        _kitolasViewModel.ChangeSelectedStone += KitolasViewModel_ChangeSelectedStone;

        // a játékmentések kezelésének összeállítása
        _storedGameBrowserModel = new StoredGameBrowserModel(_store);
        _storedGameBrowserViewModel = new StoredGameBrowserViewModel(_storedGameBrowserModel);
        _storedGameBrowserViewModel.GameLoading += StoredGameBrowserViewModel_GameLoading;
        _storedGameBrowserViewModel.GameSaving += StoredGameBrowserViewModel_GameSaving;
    }

    private async void KitolasGameModel_GameOver(object? sender, isGameEndArgs e)
    {
        

        if (e.ifEndGame)
        {
            if (e.winner == "tie")
            {
                await DisplayAlert("Kitolás",
                $"The game is over: TIE!",
                "OK");
            }
            else
            {
                await DisplayAlert("Kitolás",
                $"The game is over! The winner is: {e.winner}!",
                "OK");
            }
        }
    }

    private void KitolasViewModel_NewGame(object? sender, EventArgs e)
    {
        //_kitolasGameModel.NewGame();    EZT MEG MEG KELL IRNI A GAMEMODELBE HOGY KONSTRUALJON GAME-ET!!!!!!!!!!!!!!!
        _kitolasGameModel = new Game(3, _kitolasDataAccess); 
        
    }

    private async void KitolasViewModel_LoadGame(object? sender, EventArgs e)
    {
        await _storedGameBrowserModel.UpdateAsync(); // frissítjük a tárolt játékok listáját
        await Navigation.PushAsync(new LoadGamePage
        {
            BindingContext = _storedGameBrowserViewModel
        }); // átnavigálunk a lapra
    }

    private async void KitolasViewModel_SaveGame(object? sender, EventArgs e)
    {
        await _storedGameBrowserModel.UpdateAsync(); // frissítjük a tárolt játékok listáját
        await Navigation.PushAsync(new SaveGamePage
        {
            BindingContext = _storedGameBrowserViewModel
        }); // átnavigálunk a lapra
    }

    private async void KitolasViewModel_ExitGame(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage
        {
            BindingContext = _kitolasViewModel
        }); // átnavigálunk a beállítások lapra
    }

    private async void StoredGameBrowserViewModel_GameLoading(object? sender, StoredGameEventArgs e)
    {
        await Navigation.PopAsync(); // visszanavigálunk

        // betöltjük az elmentett játékot, amennyiben van
        try
        {
            await _kitolasGameModel.LoadGameAsync(e.Name);

            // sikeres betöltés
            await Navigation.PopAsync(); // visszanavigálunk a játék táblára
            await DisplayAlert("Kitolás játék", "Sikeres betöltés.", "OK");
            _kitolasViewModel.OnPropertyChanges();
            
        }
        catch
        {
            await DisplayAlert("Kitolás játék", "Sikertelen betöltés.", "OK");
        }
    }

    private async void StoredGameBrowserViewModel_GameSaving(object? sender, StoredGameEventArgs e)
    {
        await Navigation.PopAsync(); // visszanavigálunk
        

        try
        {
            // elmentjük a játékot
            await _kitolasGameModel.SaveGameAsync(e.Name);
            await DisplayAlert("Kitolás játék", "Sikeres mentés.", "OK");
        }
        catch
        {
            await DisplayAlert("Kitolás játék", "Sikertelen mentés.", "OK");
        }
    }

    private void KitolasViewModel_MapChange(object? sender, MapChangeEventArgs e)
    {
        _kitolasGameModel = e.Game;
        _kitolasGameModel.gameEnd += KitolasGameModel_GameOver;
    }

    private void KitolasViewModel_Slide(object? sender, SlideMoveArgs e)
    {

        switch (e.Dir)
        {
            case Keys.Left:
                _kitolasGameModel.SlideMove(Keys.Left);
                break;

            case Keys.Right:
                _kitolasGameModel.SlideMove(Keys.Right);
                break;

            case Keys.Up:
                _kitolasGameModel.SlideMove(Keys.Up);
                break;

            case Keys.Down:
                _kitolasGameModel.SlideMove(Keys.Down);

                break;

            default:
                return;
        }

    }

    private void KitolasViewModel_ChangeSelectedStone(object? sender, EventArgs e)
    {
        _kitolasGameModel.changeSelectedStone();
    }
}
