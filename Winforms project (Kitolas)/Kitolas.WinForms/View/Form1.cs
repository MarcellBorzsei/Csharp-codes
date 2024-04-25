using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Kitolas.Model;
using Kitolas.Persistence;

namespace Kitolas
{
    public partial class Form1 : Form
    {
        private Game? game;
        private IKitolasDataAccess _dataAccess;


        public Form1()
        {
            InitializeComponent();
            _dataAccess = new KitolasFileDataAccess();
            gameTablePB.Paint += DrawGameTable;
            this.KeyPreview = true;
            readyButton.Enabled = false;
            RestartButton.Enabled = false;


        }

        // Draws the table out
        private void DrawGameTable(object? sender, PaintEventArgs e)
        {
            if (game != null)
            {
                Graphics g = e.Graphics;
                int cellSize = 60; // Size of the cell
                int borderWidth = 4; // Wide border for the selected stone 

                for (int i = 0; i < game.slideTable.nSizeNum; i++)
                {
                    for (int j = 0; j < game.slideTable.nSizeNum; j++)
                    {
                        int cellValue = game.slideTable.gameTable[i, j];
                        int x = j * cellSize;
                        int y = i * cellSize;
                        Rectangle cellRect = new Rectangle(x, y, cellSize, cellSize);
                        Brush blackBrush = Brushes.Black;
                        Brush whiteBrush = Brushes.White;

                        if (cellValue == 1)
                        {
                            g.FillRectangle(blackBrush, cellRect);
                        }
                        else if (cellValue == 2)
                        {
                            g.FillRectangle(whiteBrush, cellRect);
                        }

                        g.DrawRectangle(Pens.Gray, cellRect);

                        Pen borderPen = new Pen(Color.IndianRed, borderWidth);

                        if (game.slideTable.currentPlayer == "black")
                        {
                            if (i == game.slideTable.blackStones[game.selectedStone].X && j == game.slideTable.blackStones[game.selectedStone].Y)
                            {
                                g.DrawRectangle(borderPen, cellRect);
                            }
                        }
                        if (game.slideTable.currentPlayer == "white")
                        {
                            if (i == game.slideTable.whiteStones[game.selectedStone].X && j == game.slideTable.whiteStones[game.selectedStone].Y)
                            {
                                g.DrawRectangle(borderPen, cellRect);
                            }
                        }



                    }
                }

            }
        }



        private void UpdatePictureBox()
        {
            gameTablePB.Refresh(); // Trigger the Paint event
        }

        // If game starts then game size buttons are enabled
        private void EnableButtons()
        {
            gameSetButton3.Enabled = false;
            gameSetButton4.Enabled = false;
            gameSetButton6.Enabled = false;
            readyButton.Enabled = false;
        }

        //Changes the next player dynamicly
        private void playerDisplay()
        {
            if (game?.slideTable.currentPlayer == "black")
            {
                playerTrackerLabel.Text = "Black player's turn";
            }
            if (game?.slideTable.currentPlayer == "white")
            {
                playerTrackerLabel.Text = "White player's turn";
            }
        }

        //Displays all the information found on the labels
        private void labelDisplays()
        {
            whiteStoneCounterLabel.Text = "White stones left: " + game?.slideTable.whiteCounter;
            blackStoneCounterLabel.Text = "Black stones left: " + game?.slideTable.blackCounter;
            circleNumLabel.Text = "The number of turns left: " + game?.slideTable.circleNum;


            playerDisplay();

            /*blackStoneSit.Text = "";
            whiteStoneSit.Text = "";
            for (int i = 0; i < game.slideTable.blackStones.Count; i++)
            {
                blackStoneSit.Text += game.slideTable.blackStones[i].ToString();
            }
            for (int i = 0; i < game.slideTable.whiteStones.Count; i++)
            {
                whiteStoneSit.Text += game.slideTable.whiteStones[i].ToString();
            }

            gameTableLabel.Text = "";
            for (int i = 0; i < game.slideTable.nSizeNum; i++)
            {
                for (int j = 0; j < game.slideTable.nSizeNum; j++)
                {
                    gameTableLabel.Text += game.slideTable.gameTable[i, j];
                }
                gameTableLabel.Text += "\n";
            }*/



        }


        private void onChangeDisplay(object? sender, EventArgs e)
        {
            labelDisplays();
        }

        // While game difficulty choosing to-dos
        private void displayButton_Click(object? sender, EventArgs e)
        {
            readyButton.Enabled = true;
            Button? b = sender as Button;
            if (b == null)
            {
                return;
            }
            int gameSize = int.Parse(b.Text);
            game = Game.initializeMap(gameSize, _dataAccess);
            
            game.changedDisplay += onChangeDisplay;
            UpdatePictureBox(); // Update the game table display
        }

        // Game starting to-dos
        private void gameStart(object? sender, EventArgs e)
        {
            if(game == null)
            {
                return;
            }
            game.gameEnd += onGameEnd;
            EnableButtons();
            labelDisplays();
        }


        private void onGameEnd(object? sender, isGameEndArgs e)
        {
            if(game == null)
            {
                return;
            }

            if (e.ifEndGame)
            {
                UpdatePictureBox();
                if (e.winner == "tie")
                {
                    MessageBox.Show("Tie!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The winner is: " + e.winner + "!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.KeyDown -= Form1_KeyDown;
                game.gameEnd -= onGameEnd;
                game.changedDisplay -= onChangeDisplay;
                RestartButton.Enabled = true;
            }
            

        }

        private void couldMove()
        {
            if (game == null)
            {
                return;
            }

            if (!game.checkEndGame())
            {
                UpdatePictureBox();
                playerDisplay();
            }
        }

        // If the right key is pushed, keeps the game going
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (game == null)
            {
                return;
            }

            if (e.KeyCode == Keys.Space)
            {
                game.changeSelectedStone();
                UpdatePictureBox();
            }



            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (game.SlideMove(Key.Left))
                    {
                        couldMove();
                    }
                    break;
                case Keys.Right:
                    if (game.SlideMove(Key.Right))
                    {
                        couldMove();
                    }
                    break;
                case Keys.Up:
                    if (game.SlideMove(Key.Up))
                    {
                        couldMove();
                    }
                    break;
                case Keys.Down:
                    if (game.SlideMove(Key.Down))
                    {
                        couldMove();
                    }

                    break;

                default:
                    return;
            }


        }

        private void RestartButton_Click(object? sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }

            game.Restart();
            UpdatePictureBox();
            labelDisplays();
            this.KeyDown += Form1_KeyDown;
            game.gameEnd += onGameEnd;
            game.changedDisplay += onChangeDisplay;
            RestartButton.Enabled = false;
        }

        private async void SaveStripButton_Click(object? sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await game.SaveGameAsync(_saveFileDialog.FileName);
                }
                catch (KitolasDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void LoadStripButton_Click(object? sender, EventArgs e)
        {

            if (_openFileDialog.ShowDialog() == DialogResult.OK) // ha kiválasztottunk egy fájlt
            {
                try
                {
                    // játék betöltése
                    if(game == null)
                    {
                        game = Game.initializeMap(3, _dataAccess);
                        game.changedDisplay += onChangeDisplay;

                    }
                    await game.LoadGameAsync(_openFileDialog.FileName);
                    //_menuFileSaveGame.Enabled = true;
                }
                catch (KitolasDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                UpdatePictureBox();
                labelDisplays();

                if(game == null)
                {
                    return;
                }
                game.changedDisplay += onChangeDisplay;
                if (readyButton.Enabled == false)
                {
                    readyButton.Enabled = true;
                }
                if(RestartButton.Enabled == true)
                {
                    RestartButton.Enabled = false;
                    this.KeyDown += Form1_KeyDown;
                }
                
            }

        }
    }
}