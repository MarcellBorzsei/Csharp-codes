using Kitolas.Model;
using Kitolas.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.WPF.ViewModel
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

        public DelegateCommand MapChangeCommand { get; private set; }

        public DelegateCommand SlideCommand { get; private set; }

        public DelegateCommand ChangeSelectedStoneCommand { get; private set; }

        public ObservableCollection<KitolasField> Fields { get; set; }



        
        public int CircleNum { get { return _model.slideTable.circleNum; } }
        public int SelectedStone { get { return _model.selectedStone; } }
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
        /*
         * 
         *  private int _selectedStone;
        public int SelectedStone
        {
            get { return (_selectedStone); }
            private set
            {
                _selectedStone = value;
                OnPropertyChanged(nameof(SelectedStone));
            }
        }
        private int _circleNum;
        public int CircleNum
        {
            get { return _circleNum; }
            private set
            {
                _model.slideTable.circleNum = value;
                OnPropertyChanged(nameof(CircleNum));
            }
        }

        private string? _currentPlayer;
        public string CurrentPlayer
        {
            get { return _model.slideTable.currentPlayer; }
            private set
            {
                _model.slideTable.currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        private int _blackCounter;
        public int BlackCounter
        {
            get { return _model.slideTable.blackCounter; }
            private set
            {
                _blackCounter = value;
                OnPropertyChanged(nameof(BlackCounter));
            }
        }

        private int _whiteCounter;
        public int WhiteCounter
        {
            get { return _model.slideTable.whiteCounter; }
            private set
            {
                _whiteCounter = value;
                OnPropertyChanged(nameof(WhiteCounter));
            }
        }*/

        #endregion

        #region Events

        public event EventHandler<SlideMoveArgs>? Slide;
        public event EventHandler? ChangeSelectedStone;
        public event EventHandler<MapChangeEventArgs>? MapChange;
        public event EventHandler? LoadGame;
        public event EventHandler? SaveGame;

        #endregion

        #region Contructor
        public KitolasViewModel(Game newModel)
        {
            _model = newModel;
            _dataAccess = new KitolasFileDataAccess();


            SlideCommand = new DelegateCommand(param => OnSlide(param?.ToString() ?? String.Empty));
            ChangeSelectedStoneCommand = new DelegateCommand(param => OnChangeSelectedStone());
            MapChangeCommand = new DelegateCommand(param => OnMapChange(Convert.ToInt32(param)));
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());


            
            Fields = new ObservableCollection<KitolasField>();
            
            for (int i = 0; i < _model.slideTable.nSizeNum; i++) 
            {
                for (int j = 0; j < _model.slideTable.nSizeNum; j++)
                {
                    Fields.Add(new KitolasField
                    {
                        Stone = Convert.ToString(_model.slideTable.gameTable[i, j]),
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
            

            MapChange?.Invoke(this, new MapChangeEventArgs(_model));
        }

        #endregion

    }

}
