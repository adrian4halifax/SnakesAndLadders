using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakesAndLadders
{
    public partial class MainWindow
    {
        public void RefreshPreferencesAppliedState()
        {
            // Placeholder for now — will be used as preferences expand
        }


        public void ApplyTokenSettings(TokenMode mode)
        {
            CurrentTokenMode = mode;

            switch (mode)
            {
                case TokenMode.None:
                    Player1Tokens = 0;
                    Player2Tokens = 0;
                    UpdateTokenDisplay(1);
                    UpdateTokenDisplay(2);
                    break;

                case TokenMode.Three:
                    Player1Tokens = 3;
                    Player2Tokens = 3;
                    UpdateTokenDisplay(1);
                    UpdateTokenDisplay(2);
                    break;

                case TokenMode.Infinite:
                    Player1Tokens = int.MaxValue;
                    Player2Tokens = int.MaxValue;
                    UpdateTokenDisplay(1);
                    UpdateTokenDisplay(2);
                    break;
            }

            // Update the label text
            P1Tokens.Content = mode == TokenMode.Infinite ? "Player 1 Tokens (∞)" : "Player 1 Tokens";
            P2Tokens.Content = mode == TokenMode.Infinite ? "Player 2 Tokens (∞)" : "Player 2 Tokens";
        }


        public void ApplyCounterColours(string p1Color, string p2Color)
        {
            UpdatePlayerCounterImages(1, p1Color);
            UpdatePlayerCounterImages(2, p2Color);
        }


        private void UpdatePlayerCounterImages(int player, string colorName)
        {
            string path = $"Images/Counters/Counter{colorName}.png";
            var image = new BitmapImage(new Uri(path, UriKind.Relative));

            if (player == 1)
            {
                P1Counter.Source = image;
                P1TurnCounter.Source = image;
            }
            else
            {
                P2Counter.Source = image;
                P2TurnCounter.Source = image;
            }

            UpdateTurnCounterTextColor(player, colorName);
        }


        private void UpdateTurnCounterTextColor(int player, string colorName)
        {
            bool pale = colorName == "Yellow" || colorName == "Orange";
            var text = player == 1 ? P1GoButton : P2GoButton;

            text.Foreground = pale ? Brushes.Black : Brushes.White;
        }


        public void SetPlayerBoard(int player, string boardName)
        {
            int level = int.Parse(boardName);

            int[] selectedBoard = BoardLevels[level];
            string imagePath = BoardImages[level];

            if (player == 1)
            {
                P1BoardLevel = level;
                Player1CurrentBoardName = boardName;
                currentBoardP1 = selectedBoard;
                LeftBoardImage.Source = new BitmapImage(
                    new Uri(imagePath, UriKind.Relative));
               
            }
            else
            {
                P2BoardLevel = level;
                Player2CurrentBoardName = boardName;
                currentBoardP2 = selectedBoard;
                RightBoardImage.Source = new BitmapImage(
                    new Uri(imagePath, UriKind.Relative));
               
            }
        }

    }
}
