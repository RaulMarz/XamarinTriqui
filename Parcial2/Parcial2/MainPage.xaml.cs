using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Parcial2
{
    public partial class MainPage : ContentPage
    {
        public bool isPlayersTurn = true;
        public int numberPlays = 0;
        public int humanWins = 0;
        public int computerWins = 0;
        public int ties = 0;
        public bool hasWin = false;
        public int[,] arrayPos;

        public MainPage()
        {
            InitializeComponent();
            reset();
        }

        private void reset()
        {
            arrayPos = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            isPlayersTurn = true;
            hasWin = false;
            numberPlays = 0;
            lblInfo.Text = "Cantidad de juagadas: " + numberPlays;

            btn00.Text = "-";
            btn01.Text = "-";
            btn02.Text = "-";

            btn10.Text = "-";
            btn11.Text = "-";
            btn12.Text = "-";

            btn20.Text = "-";
            btn21.Text = "-";
            btn22.Text = "-";
        }

        private void btnClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            char[] posPlayed = btn.ClassId.ToString().Replace("btn", "").ToCharArray();

            int posA = Int16.Parse(posPlayed[0].ToString());
            int posB = Int16.Parse(posPlayed[1].ToString());

            numberPlays++;

            if (arrayPos[posA, posB] != 0 && arrayPos[posA, posB] != 10)
            {
                arrayPos[posA, posB] = isPlayersTurn ? 10 : 0;

                if (isPlayersTurn)
                {
                    btn.Text = "X";
                }
                else
                {
                    btn.Text = "O";
                }

                checkWins();
                endTurn();
            }
        }

        async private void checkWins()
        {

            //Horizontal
            if (arrayPos[0, 0] == arrayPos[0, 1] && arrayPos[0, 0] == arrayPos[0, 2] )
            {
                hasWin = true;
            }
            else if (arrayPos[1, 0] == arrayPos[1, 1] && arrayPos[1, 0] == arrayPos[1, 2])
            {
                hasWin = true;
            }
            else if (arrayPos[2, 0] == arrayPos[2, 1] && arrayPos[2, 0] == arrayPos[2, 2])
            {
                hasWin = true;
            }

            //Vertical
            if (arrayPos[0, 0] == arrayPos[1, 0] && arrayPos[0, 0] == arrayPos[2, 0])
            {
                hasWin = true;
            }
            else if (arrayPos[0, 1] == arrayPos[1, 1] && arrayPos[0, 1] == arrayPos[2, 1])
            {
                hasWin = true;
            }
            else if (arrayPos[0, 2] == arrayPos[1, 2] && arrayPos[0, 2] == arrayPos[2, 2])
            {
                hasWin = true;
            }

            //Diag
            if (arrayPos[0, 0] == arrayPos[1, 1] && arrayPos[0, 0] == arrayPos[2, 2])
            {
                hasWin = true;
            }
            else if (arrayPos[0, 2] == arrayPos[1, 1] && arrayPos[0, 2] == arrayPos[2, 0])
            {
                hasWin = true;
            }

            if (hasWin)
            {
                if (isPlayersTurn)
                {
                    humanWins++;
                }
                else
                {
                    computerWins++;
                }
                await ShowMessage("Yay!!", isPlayersTurn ? "Human won!" : "Computer Won", "Ok", async () =>
                {
                    reset();
                });
            }

            if (!hasWin && numberPlays == 9)
            {
                ties++;
                await ShowMessage("Ohh!!",  "Draw Game!", "Ok", async () =>
                {
                    reset();
                });
            }
        }

        private void playersTurn()
        {
            computersTurn();
        }

        private void computersTurn()
        {

        }

        private void endTurn()
        {
            isPlayersTurn = !isPlayersTurn;
            
            lblInfo.Text = "Cantidad de juagadas: " + numberPlays;
            lblHuman.Text = "Human: " + humanWins;
            lblTies.Text = "Ties: " + ties;
            lblAndroid.Text = "Android: " + computerWins;
        }

        public async Task ShowMessage(string title,
            string message,
            string buttonText,
            Action afterHideCallback)
        {
            await DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }
    }
}
