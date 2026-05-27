using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SnakesAndLadders
{
    public partial class MainWindow
    {
        private void P1GoButton_Click(object sender, RoutedEventArgs e)
        {
            RollDie();
            SetGoButtonVisibility(1, false);
        }

        private void P2GoButton_Click(object sender, RoutedEventArgs e)
        {
            RollDie();
            SetGoButtonVisibility(2, false);
        }

        private async void RollDie()
        {
            SetTrayOpacity(1.0);
            PlaySound("die");
            await Task.Delay(180);
            SetDieFace("dieBlank.png");

            int rollValue = RollAndStoreDie();
            await Task.Delay(120);
            SetDieFace($"die{rollValue}.png");

            SetCounterButtonVisibility();
        }

        private void SetTrayOpacity(double opacity)
        {
            P1TrayCanvas.Opacity = opacity;
            P2TrayCanvas.Opacity = opacity;
            CounterTrayLabel.Opacity = opacity;
        }

        private void SetDieFace(string fileName)
        {
            DieFace.Source = new BitmapImage(new Uri($"Images/DieFaces/{fileName}", UriKind.Relative));
        }

        private int RollAndStoreDie()
        {
            int value = _rng.Next(1, 7);
            roll = value;
            _lastDieRoll = value;
            return value;
        }

        private void SetGoButtonVisibility(int player, bool visible)
        {
            if (player == 1)
                P1GoButton.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
            else
                P2GoButton.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SetCounterButtonVisibility()
        {
            P1CounterButton.Visibility = Visibility.Collapsed;
            P2CounterButton.Visibility = Visibility.Collapsed;

            if (_currentPlayer == 1)
            {
                P1CounterButton.Visibility = Visibility.Visible;
                P2CounterButton.Visibility = Player1HasTokens ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (_currentPlayer == 2)
            {
                P2CounterButton.Visibility = Visibility.Visible;
                P1CounterButton.Visibility = Player2HasTokens ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private async void Player1Counter_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPlayer == 1)
            {
                await MoveCounterForPlayerAsync(1, 1);
                P2CounterButton.Visibility = Visibility.Collapsed;
            }
            else if (_currentPlayer == 2 && Player2Tokens > 0)
            {
                Player2Tokens--;
                UpdateTokenDisplay(2);
                await MoveCounterForPlayerAsync(1, 1); // Move Player 1's counter
            }
        }

        private async void Player2Counter_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPlayer == 2)
            {
                await MoveCounterForPlayerAsync(2, 2);
            }
            else if (_currentPlayer == 1 && Player1Tokens > 0)
            {
                Player1Tokens--;
                UpdateTokenDisplay(1);
                await MoveCounterForPlayerAsync(2, 2); // Move Player 2's counter
            }
        }
        private void SwitchTurn()
        {
            if (_currentPlayer == 1)
            {
                // Switch to Player 2
                P1TurnCounter.Opacity = 0.4;
                P2TurnCounter.Opacity = 1.0;
                P1GoButton.Visibility = Visibility.Collapsed;
                P2GoButton.Visibility = Visibility.Visible;
                _currentPlayer = 2;
            }
            else
            {
                // Switch to Player 1
                P1TurnCounter.Opacity = 1.0;
                P2TurnCounter.Opacity = 0.4;
                P2GoButton.Visibility = Visibility.Collapsed;
                P1GoButton.Visibility = Visibility.Visible;
                _currentPlayer = 1;
            }
        }

        private void UpdateTokenDisplay(int player)
        {
            if (player == 1)
            {
                P1Token1.Visibility = Player1Tokens >= 1 ? Visibility.Visible : Visibility.Collapsed;
                P1Token2.Visibility = Player1Tokens >= 2 ? Visibility.Visible : Visibility.Collapsed;
                P1Token3.Visibility = Player1Tokens >= 3 ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                P2Token1.Visibility = Player2Tokens >= 1 ? Visibility.Visible : Visibility.Collapsed;
                P2Token2.Visibility = Player2Tokens >= 2 ? Visibility.Visible : Visibility.Collapsed;
                P2Token3.Visibility = Player2Tokens >= 3 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

    }
}