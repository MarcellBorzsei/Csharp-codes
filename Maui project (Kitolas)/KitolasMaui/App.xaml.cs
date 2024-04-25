using Kitolas.Persistence;
using KitolasMaui.ViewModel;
using Kitolas.Model;
using KitolasMaui.Persistence;

namespace KitolasMaui;

public partial class App : Application
{

    private const string SuspendedGameSavePath = "SuspendedGame";

    private readonly AppShell _appShell;
    private readonly IKitolasDataAccess _kitolasDataAccess;
    private readonly Game _kitolasGameModel;
    private readonly IStore _kitolasStore;
    private readonly KitolasViewModel _kitolasViewModel;

    public App()
	{
        InitializeComponent();

        _kitolasStore = new KitolasStore();
        _kitolasDataAccess = new KitolasFileDataAccess(FileSystem.AppDataDirectory);

        _kitolasGameModel = new Game(3, _kitolasDataAccess);
        _kitolasViewModel = new KitolasViewModel(_kitolasGameModel);

        _appShell = new AppShell(_kitolasStore, _kitolasDataAccess, _kitolasGameModel, _kitolasViewModel)
        {
            BindingContext = _kitolasViewModel
        };
        MainPage = _appShell;
    }

}
