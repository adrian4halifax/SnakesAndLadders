using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SnakesAndLadders
{
    public partial class MainWindow
    {

        private void ResetCore()
        {
            // Reset board levels
            P1BoardLevel = 1;
            P2BoardLevel = 1;

            // Reset board arrays
            currentBoardP1 = level1Board;
            currentBoardP2 = level1Board;

            // Reset board images
            LeftBoardImage.Source = new BitmapImage(new Uri("Images/Boards/BoardLevel1.png", UriKind.Relative));
            RightBoardImage.Source = new BitmapImage(new Uri("Images/Boards/BoardLevel1.png", UriKind.Relative));

            // Reset board positions
            P1BoardPosition = 0;
            P2BoardPosition = 0;

            // Reset portal departure squares
            P1SidePortSquare = 1;
            P2SidePortSquare = 1;

            // Remove counters from boards
            LeftBoardCanvas.Children.Remove(P1Counter);
            LeftBoardCanvas.Children.Remove(P1CounterButton);
            RightBoardCanvas.Children.Remove(P2Counter);
            RightBoardCanvas.Children.Remove(P2CounterButton);

            // Return counters to trays
            if (!P1TrayCanvas.Children.Contains(P1Counter))
                P1TrayCanvas.Children.Add(P1Counter);

            if (!P1TrayCanvas.Children.Contains(P1CounterButton))
                P1TrayCanvas.Children.Add(P1CounterButton);

            if (!P2TrayCanvas.Children.Contains(P2Counter))
                P2TrayCanvas.Children.Add(P2Counter);

            if (!P2TrayCanvas.Children.Contains(P2CounterButton))
                P2TrayCanvas.Children.Add(P2CounterButton);

            // Grey out trays
            P1TrayCanvas.Opacity = 0.5;
            P2TrayCanvas.Opacity = 0.5;

            // Reset counter opacity
            P1Counter.Opacity = 1.0;
            P2Counter.Opacity = 1.0;

            // Reset counter positions inside trays
            Canvas.SetLeft(P1Counter, 0);
            Canvas.SetTop(P1Counter, 0);
            Canvas.SetLeft(P1CounterButton, 0);
            Canvas.SetTop(P1CounterButton, 0);

            Canvas.SetLeft(P2Counter, 0);
            Canvas.SetTop(P2Counter, 0);
            Canvas.SetLeft(P2CounterButton, 0);
            Canvas.SetTop(P2CounterButton, 0);

            // Reset tokens (logic + UI)
            Player1Tokens = 3;
            Player2Tokens = 3;

            P1Token1.Visibility = Visibility.Visible;
            P1Token2.Visibility = Visibility.Visible;
            P1Token3.Visibility = Visibility.Visible;

            P1Token1.Opacity = 1.0;
            P1Token2.Opacity = 1.0;
            P1Token3.Opacity = 1.0;

            P2Token1.Visibility = Visibility.Visible;
            P2Token2.Visibility = Visibility.Visible;
            P2Token3.Visibility = Visibility.Visible;

            P2Token1.Opacity = 1.0;
            P2Token2.Opacity = 1.0;
            P2Token3.Opacity = 1.0;

            Player1HasTokens = true;
            Player2HasTokens = true;
        }


        private void ResetGame()
        {
            ResetCore();

            // Reset die
            DieFace.Source = new BitmapImage(new Uri("Images/DieFaces/die1.png", UriKind.Relative));

            // Reset turn indicators
            P1TurnCounter.Opacity = 1.0;
            P2TurnCounter.Opacity = 0.4;

            // Reset Go buttons
            P1GoButton.Visibility = Visibility.Visible;
            P2GoButton.Visibility = Visibility.Collapsed;

            // Reset counter button visibility
            P1CounterButton.Visibility = Visibility.Collapsed;
            P2CounterButton.Visibility = Visibility.Collapsed;

            // Reset current player
            _currentPlayer = 1;
        }

        public void ResetForPreferences()
        {
            // Preferences-specific resets (if any)
            Player1CurrentBoardName = "1";
            Player2CurrentBoardName = "1";
        }

    }
}