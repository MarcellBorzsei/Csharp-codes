using Kitolas.Model;
using Kitolas.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitolasMaui.ViewModel
{
    public class KitolasViewModel : ViewModelBase
    {
        #region Fields

        private Game _model;
        private IKitolasDataAccess _dataAccess;

        #endregion

        #region Properties
        public DelegateCommand LoadGameCommand { get; private set; }

        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand ExitGameCommand { get; private set; }

        public DelegateCommand NewGameCommand { get; private set; }

        public DelegateCommand MapChangeCommand { get; private set; }

        public DelegateCommand SlideCommand { get; private set; }

        public DelegateCommand ChangeSelectedStoneCommand { get; private set; }

        public ObservableCollection<KitolasField> Fields { get; set; }




        public int CircleNum { get { return _model.slideTable.circleNum; } }
        public int SelectedStone { get { return _model.selectedStone+1; } }
        public string CurrentPlayer { get { return _model.slideTable.currentPlayer; } }
        public int BlackCounter { get { return _model.slideTable.blackCounter; } }
        public int WhiteCounter { get { return _model.slideTable.whiteCounter; } }




        private int _mapSize;
        public int MapSize
        {
            get { return _mapSize; }
            set
            {
                if (_mapSize != value)
                {
                    _mapSize = value;
                    RefreshTable();
                    OnPropertyChanged(nameof(MapSize));
                }
            }
        }

        public RowDefinitionCollection GameTableRows
        {
            get => new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition(GridLength.Star), MapSize).ToArray());
        }


        /// <summary>
        /// Segédproperty a tábla méretezéséhez
        /// </summary>
        public ColumnDefinitionCollection GameTableColumns
        {
            get => new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition(GridLength.Star), MapSize).ToArray());
        }
        

        #endregion

        #region Events

        public event EventHandler<SlideMoveArgs>? Slide;
        public event EventHandler? ChangeSelectedStone;
        public event EventHandler<MapChangeEventArgs>? MapChange;
        public event EventHandler? LoadGame;
        public event EventHandler? SaveGame;
        public event EventHandler? ExitGame;
        public event EventHandler? NewGame;

        #endregion

        #region Contructor
        public KitolasViewModel(Game newModel)
        {
            _model = newModel;
            _dataAccess = new KitolasFileDataAccess(FileSystem.AppDataDirectory);


            SlideCommand = new DelegateCommand(param => OnSlide(param?.ToString() ?? String.Empty));
            ChangeSelectedStoneCommand = new DelegateCommand(param => OnChangeSelectedStone());
            MapChangeCommand = new DelegateCommand(param => OnMapChange(Convert.ToInt32(param)));
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitGameCommand = new DelegateCommand(param => OnExitGame());
            NewGameCommand = new DelegateCommand(param => OnNewGame());



            Fields = new ObservableCollection<KitolasField>();

            MapSize = 3;

            for (int i = 0; i < _model.slideTable.nSizeNum; i++)
            {
                for (int j = 0; j < _model.slideTable.nSizeNum; j++)
                {
                    Fields.Add(new KitolasField
                    {
                        Stone = Convert.ToString(_model.slideTable.gameTable[i, j]),
                        X = i,
                        Y = j,
                        BackgroundColor = (_model.slideTable.gameTable[i, j] == 1) ? Colors.Black :
(_model.slideTable.gameTable[i, j] == 2) ? Colors.White :
Colors.Grey,
                    });
                }
            }

        }

        #endregion

        #region Private Methods

        private void RefreshTable()
        {
            Fields.Clear();

            for (int i = 0; i < _model.slideTable.nSizeNum; i++) // inicializáljuk a mezőket
            {
                for (int j = 0; j < _model.slideTable.nSizeNum; j++)
                {
                    Fields.Add(new KitolasField
                    {
                        Stone = Convert.ToString(_model.slideTable.gameTable[i, j]),
                        X = i,
                        Y = j,
                        BackgroundColor = (_model.slideTable.gameTable[i, j] == 1) ? Colors.Black :
(_model.slideTable.gameTable[i, j] == 2) ? Colors.White :
Colors.Grey,

                    });
                }
            }

            OnPropertyChanged(nameof(MapSize));
            OnPropertyChanged(nameof(CircleNum));
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(SelectedStone));
            OnPropertyChanged(nameof(BlackCounter));
            OnPropertyChanged(nameof(WhiteCounter));
        }

        public void OnPropertyChanges()
        {
            MapSize = _model.slideTable.nSizeNum;
            //ide a tobbit

            OnPropertyChanged(nameof(MapSize));
            OnPropertyChanged(nameof(CircleNum));
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(SelectedStone));
            OnPropertyChanged(nameof(BlackCounter));
            OnPropertyChanged(nameof(WhiteCounter));
            OnPropertyChanged(nameof(GameTableRows));
            OnPropertyChanged(nameof(GameTableColumns));
        }


        private void OnNewGame()
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnExitGame()
        {
            ExitGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoadGame()
        {
            LoadGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveGame()
        {
            SaveGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnSlide(String dir)
        {
            Slide?.Invoke(this, new SlideMoveArgs(dir));
            RefreshTable(); // For refreshing before the game ends
            _model.checkEndGame();
            RefreshTable(); // For refreshing if restart button is pressed, displaying the new game
        }


        private void OnChangeSelectedStone()
        {
            ChangeSelectedStone?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(SelectedStone));
        }

        private void OnMapChange(int newSize)
        {
            _model = Game.initializeMap(newSize, _dataAccess);
            MapSize = newSize;

            OnPropertyChanged(nameof(GameTableRows));
            OnPropertyChanged(nameof(GameTableColumns));
            OnPropertyChanged(nameof(MapSize));
            


            MapChange?.Invoke(this, new MapChangeEventArgs(_model));
        }

        #endregion

    }
}
