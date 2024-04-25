using Kitolas.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.Model
{
        
    public class Game
    {
        #region Events
        public event EventHandler<EventArgs>? changedDisplay;
        public event EventHandler<isGameEndArgs>? gameEnd;
        #endregion 
        
        #region Fields
       


        private IKitolasDataAccess _dataAccess;
        public SlideTable slideTable { get; private set; }
        public int selectedStone { get; set; }
        
        
        #endregion

        #region Constructor
        public Game(int n, IKitolasDataAccess dataAccess)
        {
            slideTable = new SlideTable(n);
            _dataAccess = dataAccess;
            selectedStone = 0;
        }

        // new game constructor for fixed spotted stones
        public Game(SlideTable table, IKitolasDataAccess dataAccess)
        {
            slideTable = table;
            _dataAccess = dataAccess;
        }
        #endregion

        #region Public methods
        public static Game initializeMap(int gameSize, IKitolasDataAccess dataAccess)
        {

            switch (gameSize)
            {
                case 3:
                    return new Game(3, dataAccess);


                case 4:
                    return new Game(4, dataAccess);


                case 6:
                    return new Game(6, dataAccess);


                default:
                    return new Game(3, dataAccess); // return null
            }
        }


        // Writes out the element of the matrix
        /*private void printGameTable()
        {
            for (int i = 0; i < slideTable.nSizeNum; i++)
            {
                for (int j = 0; j < slideTable.nSizeNum; j++)
                {
                    Console.Write(slideTable.gameTable[i, j]);
                }
                Console.WriteLine();
            }
        }*/

        
        public void changeSelectedStone()
        {
            
            if (slideTable.currentPlayer == "black")
            {
                selectedStone = ((selectedStone + 1) % slideTable.blackCounter);
            }
            else
            {
                selectedStone = ((selectedStone + 1) % slideTable.whiteCounter);
            }
        }

        public bool SlideMove(Key d)
        {
            bool ifSuccessful = false;
            if (slideTable.currentPlayer == "black")
            {
                ifSuccessful = slideTable.slide(slideTable.blackStones[selectedStone].X + 1, slideTable.blackStones[selectedStone].Y + 1, d);
                changedDisplay?.Invoke(null, EventArgs.Empty);
                
                //return ifSuccessful;
            }
            else
            {
                ifSuccessful = slideTable.slide(slideTable.whiteStones[selectedStone].X + 1, slideTable.whiteStones[selectedStone].Y + 1, d);
                changedDisplay?.Invoke(null, EventArgs.Empty);
                //return ifSuccessful;

            }

            if(ifSuccessful)
            {
                if(slideTable.blackCounter !=0 && slideTable.whiteCounter != 0)
                {
                    changePlayer();
                }                
            }
            return ifSuccessful;

        }

        private void changePlayer()
        {
            if (slideTable.currentPlayer == "black")
            {
                slideTable.currentPlayer = "white";
            }
            else
            {
                slideTable.currentPlayer = "black";
            }
            selectedStone = 0;

        }

        public bool checkEndGame()
        {
            if (slideTable.blackCounter == 0 || (slideTable.circleNum == 0 && slideTable.whiteCounter > slideTable.blackCounter))
            {
                //signal a fekete vesztett
                gameEnd?.Invoke(this, new isGameEndArgs { ifEndGame = true, winner = "white" });
                return true;
            }
            else if (slideTable.whiteCounter == 0 || (slideTable.circleNum == 0 && slideTable.whiteCounter < slideTable.blackCounter))
            {
                //signal a feher vesztett
                gameEnd?.Invoke(this, new isGameEndArgs { ifEndGame = true, winner = "black" });
                return true;
            }
            else if (slideTable.circleNum == 0 && slideTable.whiteCounter == slideTable.blackCounter)
            {
                gameEnd?.Invoke(this, new isGameEndArgs { ifEndGame = true, winner = "tie" });
                return true;
            }
            else
            {
                //nincs nyertes
                gameEnd?.Invoke(this, new isGameEndArgs { ifEndGame = false, winner = "" });
                return false;
            }
        }

        public void Restart()
        {
            slideTable.NewGameInitialize();
            changedDisplay?.Invoke(null, EventArgs.Empty);
        }

        public async Task SaveGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, slideTable);
        }

        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            slideTable = await _dataAccess.LoadAsync(path);
        }    

        #endregion

    }

}
