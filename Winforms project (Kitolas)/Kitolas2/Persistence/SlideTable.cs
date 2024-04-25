using Kitolas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kitolas.Persistence
{
    public class SlideTable
    {
        #region Fields
        public int nSizeNum { get; } // size of the table

        public int circleNum { get; set; } // number of circles
        public int[,] gameTable { get; private set; } // table of the game
        public int blackCounter { get; private set; } // counter of black stones 

        public int whiteCounter { get; private set; } // counter of white stones

        public List<Point> whiteStones { get; private set; } // List of the white stones' coordinates

        public List<Point> blackStones { get; private set; } // List of the black stones' coordinates

        public string? currentPlayer { get; set; } //Starts from black, always starts
        #endregion

        #region Constructor
        public SlideTable(int n)
        {

            //declaring the essential variables
            nSizeNum = n;
            circleNum = nSizeNum * 5;
            gameTable = new int[nSizeNum, nSizeNum];
            blackCounter = whiteCounter = nSizeNum;
            whiteStones = new List<Point>();
            blackStones = new List<Point>();
            currentPlayer = "black";


            //fill the game table matrix with zeros
            for (int i = 0; i < nSizeNum; i++)
            {
                for (int j = 0; j < nSizeNum; j++)
                {
                    gameTable[i, j] = 0;
                }
            }


            //filling the game table matrix with the black (1) and white (2) stones 
            Random rnd = new Random();
            int blackStoneCounter = 0;

            while (blackStoneCounter != nSizeNum)
            {

                int row = rnd.Next(0, nSizeNum);
                int col = rnd.Next(0, nSizeNum);
                if (gameTable[row, col] == 0)
                {
                    gameTable[row, col] = 1; // ones will be the black stones
                    //Console.WriteLine($"sikeres ({row}, {col}) -- {blackStoneCounter}, {nSizeNum}");
                    blackStoneCounter++;
                }
            }

            int whiteStoneCounter = 0;

            while (whiteStoneCounter != nSizeNum)
            {

                int row2 = rnd.Next(0, nSizeNum);
                int col2 = rnd.Next(0, nSizeNum);
                if (gameTable[row2, col2] == 0)
                {
                    gameTable[row2, col2] = 2; // twos will be the white stones
                    //Console.WriteLine($"sikeres2 ({row2}, {col2})");
                    whiteStoneCounter++;
                }
            }
            stoneListFiller();

        }

        // new constructor for testing (creates a game without randomization)
        public SlideTable(int n, List<Point> _blackStones, List<Point> _whiteStones)
        {
            whiteStones = new List<Point>();
            blackStones = new List<Point>();
            /*if (_blackStones.Count != n && _whiteStones.Count != n)
            {
                new SlideTable(n);
            }*/

            //declaring the essential variables
            nSizeNum = n;
            circleNum = nSizeNum * 5;
            gameTable = new int[nSizeNum, nSizeNum];
            blackCounter = whiteCounter = nSizeNum;
            whiteStones = _whiteStones;
            blackStones = _blackStones;
            currentPlayer = "black";


            //fill the game table matrix with zeros
            for (int i = 0; i < nSizeNum; i++)
            {
                for (int j = 0; j < nSizeNum; j++)
                {
                    gameTable[i, j] = 0;
                }
            }

            for (int i = 0; i < nSizeNum; i++)
            {
                gameTable[blackStones[i].X, blackStones[i].Y] = 1;
                gameTable[whiteStones[i].X, whiteStones[i].Y] = 2;
            }

        }
        #endregion

        #region Private methods

        private void stoneListFiller()
        {
            // Filling up the list which contains the coordinates of the stones
            for (int i = 0; i < nSizeNum; i++)
            {
                for (int j = 0; j < nSizeNum; j++)
                {
                    if (gameTable[i, j] == 1)
                    {
                        blackStones.Add(new Point(i, j));
                    }
                    if (gameTable[i, j] == 2)
                    {
                        whiteStones.Add(new Point(i, j));
                    }

                }
            }
        }
        private void rearrangeStoneLists()
        {
            whiteStones = new List<Point>();
            blackStones = new List<Point>();
            stoneListFiller();
        }

        #endregion

        #region Public methods
        public bool slide(int x, int y, Key dir)
        {
            // Next to border to invalid direction
            if (x == 1 && dir == Key.Up || y == 1 && dir == Key.Left || x == nSizeNum && dir == Key.Down || y == nSizeNum && dir == Key.Right)
            {
                return false;
            }
            else
            {
                switch (dir)
                {

                    case Key.Left:
                        int stepCounter = 0;
                        int itX = x - 1;
                        int itY = y - 1;

                        while (y - 1 - stepCounter > -1 && gameTable[itX, itY] != 0)
                        {
                            itY--;
                            stepCounter++;
                        }

                        bool successfulSlide = y - stepCounter == 0;

                        if (successfulSlide)
                        {
                            //Console.WriteLine("letolas");
                            if (gameTable[x - 1, 0] == 1)
                            {
                                blackCounter--;
                            }
                            else
                            {
                                whiteCounter--;
                            }
                            gameTable[x - 1, 0] = 0;

                            for (int i = 0; i < y - 1; i++)
                            {
                                gameTable[x - 1, i] = gameTable[x - 1, i + 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                            //Console.WriteLine($"{whiteCounter}, {blackCounter}");


                        }
                        else
                        {
                            //Console.WriteLine("nemletolas");
                            for (int i = y - 1 - stepCounter; i < y - 1; i++)
                            {
                                gameTable[x - 1, i] = gameTable[x - 1, i + 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                        }
                        rearrangeStoneLists();
                        circleNum--;

                        break;

                    case Key.Up:

                        stepCounter = 0;
                        itX = x - 1;
                        itY = y - 1;

                        while (x - 1 - stepCounter > -1 && gameTable[itX, itY] != 0)
                        {
                            itX--;
                            stepCounter++;
                        }

                        successfulSlide = x - stepCounter == 0;

                        if (successfulSlide)
                        {
                            //Console.WriteLine("letolas");
                            if (gameTable[0, y - 1] == 1)
                            {
                                blackCounter--;
                            }
                            else
                            {
                                whiteCounter--;
                            }
                            gameTable[0, y - 1] = 0;

                            for (int i = 0; i < x - 1; i++)
                            {
                                gameTable[i, y - 1] = gameTable[i + 1, y - 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                            //Console.WriteLine($"{whiteCounter}, {blackCounter}");

                        }
                        else
                        {
                            //Console.WriteLine("nemletolas");
                            for (int i = x - 1 - stepCounter; i < x - 1; i++)
                            {
                                gameTable[i, y - 1] = gameTable[i + 1, y - 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                        }
                        rearrangeStoneLists();
                        circleNum--;

                        break;

                    case Key.Right:
                        stepCounter = 0;
                        itX = x - 1;
                        itY = y - 1;
                        while (y - 1 + stepCounter < nSizeNum && gameTable[itX, itY] != 0)
                        {
                            itY++;
                            stepCounter++;
                        }

                        successfulSlide = y + stepCounter == nSizeNum + 1;

                        if (successfulSlide)
                        {
                            //Console.WriteLine("letolas");
                            if (gameTable[x - 1, nSizeNum - 1] == 1)
                            {
                                blackCounter--;
                            }
                            else
                            {
                                whiteCounter--;
                            }
                            gameTable[x - 1, nSizeNum - 1] = 0;

                            for (int i = nSizeNum - 1; i > y - 1; i--)
                            {
                                gameTable[x - 1, i] = gameTable[x - 1, i - 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                            //Console.WriteLine($"{whiteCounter}, {blackCounter}");

                        }
                        else
                        {
                            //Console.WriteLine("nemletolas");
                            for (int i = y - 1 + stepCounter; i > y - 1; i--)
                            {
                                gameTable[x - 1, i] = gameTable[x - 1, i - 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                            //Console.WriteLine($"{whiteCounter}, {blackCounter}");
                        }
                        
                        rearrangeStoneLists();
                        circleNum--;
                        break;

                    case Key.Down:
                        stepCounter = 0;
                        itX = x - 1;
                        itY = y - 1;
                        while (x - 1 + stepCounter < nSizeNum && gameTable[itX, itY] != 0)
                        {
                            itX++;
                            stepCounter++;
                        }

                        successfulSlide = x + stepCounter == nSizeNum + 1;

                        if (successfulSlide)
                        {
                            //Console.WriteLine("letolas");
                            if (gameTable[nSizeNum - 1, y - 1] == 1)
                            {
                                blackCounter--;
                            }
                            else
                            {
                                whiteCounter--;
                            }
                            gameTable[nSizeNum - 1, y - 1] = 0;

                            for (int i = nSizeNum - 1; i > x - 1; i--)
                            {
                                gameTable[i, y - 1] = gameTable[i - 1, y - 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                            //Console.WriteLine($"{whiteCounter}, {blackCounter}");

                        }
                        else
                        {
                            //Console.WriteLine("nemletolas");
                            for (int i = x - 1 + stepCounter; i > x - 1; i--)
                            {
                                gameTable[i, y - 1] = gameTable[i - 1, y - 1];
                            }
                            gameTable[x - 1, y - 1] = 0;
                            //Console.WriteLine($"{whiteCounter}, {blackCounter}");
                        }
                        
                        rearrangeStoneLists();
                        circleNum--;
                        break;

                }
                return true;
            }
        }

        public void NewGameInitialize()
        {
            circleNum = nSizeNum * 5;
            whiteStones = new List<Point>();
            blackStones = new List<Point>();
            blackCounter = whiteCounter = nSizeNum;
            currentPlayer = "black";

            //fill the game table matrix with zeros
            for (int i = 0; i < nSizeNum; i++)
            {
                for (int j = 0; j < nSizeNum; j++)
                {
                    gameTable[i, j] = 0;
                }
            }

            //filling the game table matrix with the black (1) and white (2) stones 
            Random rnd = new Random();
            int blackStoneCounter = 0;

            while (blackStoneCounter != nSizeNum)
            {

                int row = rnd.Next(0, nSizeNum);
                int col = rnd.Next(0, nSizeNum);
                if (gameTable[row, col] == 0)
                {
                    gameTable[row, col] = 1; // ones will be the black stones
                    blackStoneCounter++;
                }
            }

            int whiteStoneCounter = 0;

            while (whiteStoneCounter != nSizeNum)
            {

                int row2 = rnd.Next(0, nSizeNum);
                int col2 = rnd.Next(0, nSizeNum);
                if (gameTable[row2, col2] == 0)
                {
                    gameTable[row2, col2] = 2; // twos will be the white stones
                    whiteStoneCounter++;
                }
            }
            stoneListFiller();
        }

        public void LoadInitialize(int _circleNum, int _blackCounter, int _whiteCounter, string _currentPlayer)
        {
            circleNum = _circleNum;
            blackCounter = _blackCounter;
            whiteCounter = _whiteCounter;
            currentPlayer = _currentPlayer;

            rearrangeStoneLists();
        }


        

        #endregion

    }
}
