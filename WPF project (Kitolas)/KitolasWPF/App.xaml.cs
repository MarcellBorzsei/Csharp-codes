using Kitolas.Model;
using Kitolas.Persistence;
using Kitolas.WPF.ViewModel;
using KitolasWPF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kitolas.WPF
{
    public partial class App : Application
    {
        private Game _model = null!;
        private KitolasViewModel _viewModel = null!;
        private MainWindow _view = null!;
        private IKitolasDataAccess? _dataAccess;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object? sender, StartupEventArgs e)
        {

            _dataAccess = new KitolasFileDataAccess();
            _model = Game.initializeMap(3, _dataAccess);
            _model.gameEnd += _model_gameEnd; 


            _viewModel = new KitolasViewModel(_model);
            _viewModel.Slide += _viewModel_Slide;
            _viewModel.ChangeSelectedStone += _viewModel_ChangeSelectedStone;
            _viewModel.MapChange += _viewModel_MapChange;
            _viewModel.LoadGame += _viewModel_LoadGame;
            _viewModel.SaveGame += _viewModel_SaveGame;


            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();
        }

        private async void _viewModel_SaveGame(object? sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); 
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        await _model.SaveGameAsync(saveFileDialog.FileName);
                    }
                    catch (KitolasDataException)
                    {
                        MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("A fájl mentése sikertelen!", "Vadászat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void _viewModel_LoadGame(object? sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    
                    await _model.LoadGameAsync(openFileDialog.FileName);
                    _viewModel.MapSize = _model.slideTable.nSizeNum;

                }
            }
            catch (KitolasDataException)
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Vadászat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _model_gameEnd(object? sender, isGameEndArgs e)
        {
            if (e.ifEndGame)
            {

                if (e.winner == "tie")
                {
                    if (MessageBox.Show($"The game is over: TIE! Do you want to play again?", "Kitolás", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                    {
                        _model.Restart();
                    }
                    else
                    {
                        _view.Close();
                    }
                }
                else
                {
                    if (MessageBox.Show($"The game is over! The winner is: {e.winner}! Do you want to play again?", "Kitolás", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                    {
                        _model.Restart();
                    }
                    else
                    {
                        _view.Close();
                    }
                }
            }
        }



        private void _viewModel_MapChange(object? sender, MapChangeEventArgs e)
        {
            _model = e.Game;
            _model.gameEnd += _model_gameEnd;
        }

        private void _viewModel_ChangeSelectedStone(object? sender, EventArgs e)
        {
            _model.changeSelectedStone();
        }

        private void _viewModel_Slide(object? sender, SlideMoveArgs e)
        {

            switch (e.Dir)
            {
                case Key.Left:
                    _model.SlideMove(Key.Left);
                    break;

                case Key.Right:
                    _model.SlideMove(Key.Right);
                    break;

                case Key.Up:
                    _model.SlideMove(Key.Up);
                    break;

                case Key.Down:
                    _model.SlideMove(Key.Down);

                    break;

                default:
                    return;
            }
            
        }
    }
}
