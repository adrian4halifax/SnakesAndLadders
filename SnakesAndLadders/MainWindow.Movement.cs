using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SnakesAndLadders
{
    public partial class MainWindow
    {
        /// <summary>
        /// Ensures the player's counter and button are on the correct board canvas.
        /// </summary>
        /// <param name="player">Player number (1 or 2)</param>
        private void EnsureCounterOnBoard(int player)
        {
            if (player == 1)
            {
                if (!LeftBoardCanvas.Children.Contains(P1Counter))
                {
                    P1TrayCanvas.Children.Remove(P1Counter);
                    P1TrayCanvas.Children.Remove(P1CounterButton);
                    LeftBoardCanvas.Children.Add(P1Counter);
                    LeftBoardCanvas.Children.Add(P1CounterButton);
                }
            }
            else
            {
                if (!RightBoardCanvas.Children.Contains(P2Counter))
                {
                    P2TrayCanvas.Children.Remove(P2Counter);
                    P2TrayCanvas.Children.Remove(P2CounterButton);
                    RightBoardCanvas.Children.Add(P2Counter);
                    RightBoardCanvas.Children.Add(P2CounterButton);
                }
            }
        }

        private async Task MoveCounterForPlayerAsync(int player, int board)
        {
            // ===============================
            // 1. HIDE BOTH BOARD COUNTER BUTTONS DURING MOVEMENT
            // ===============================
            P1CounterButton.Visibility = Visibility.Collapsed;
            P2CounterButton.Visibility = Visibility.Collapsed;


            // ===============================
            // 2. PLACE COUNTER ON BOARD (FIRST MOVE ONLY)
            // ===============================

            EnsureCounterOnBoard(player);


            // ===============================
            // 3. STEP COUNTER ONE SQUARE AT A TIME WITH BOUNCE LOGIC
            // ===============================
            int start = (player == 1) ? P1BoardPosition : P2BoardPosition;
            int roll = _lastDieRoll;
            int target = start + roll;

            if (target <= FinalSquare)
            {
                // Simple forward movement
                await StepCounterAsync(player, roll);
            }
            else
            {
                // Bounce-back logic
                int toFinal = FinalSquare - start;
                int excess = target - FinalSquare;

                if (toFinal > 0)
                    await StepCounterAsync(player, toFinal);

                if (excess > 0)
                    await StepCounterAsync(player, -excess);

                // Update the board position manually
                if (player == 1)
                    P1BoardPosition = FinalSquare - excess;
                else
                    P2BoardPosition = FinalSquare - excess;

                UpdateCounterPosition(player);
            }


            // ===============================
            // 4. READ EFFECT FROM BOARD
            // ===============================
            int effect = (player == 1)
                ? currentBoardP1[P1BoardPosition]
                : currentBoardP2[P2BoardPosition];

            int square = (player == 1) ? P1BoardPosition : P2BoardPosition;



            // ===============================
            // 5. WIN EFFECT (1000)
            // ===============================
            if (effect == WinEffectCode)
            {
                PlaySound("win");
                await Task.Delay(2000);
                TriggerWin(player);
                return;
            }


            // ===============================
            // 6. SNAKE / LADDER SOUND (before movement)
            // ===============================
                       
            bool isMovementEffect =
                effect >= MovementEffectRangeStart &&
                effect <= MovementEffectRangeEnd;

            if (isMovementEffect)
            {
                bool isLadder = effect > square;
                bool isSnake = effect < square;

                if (isLadder)
                    PlaySound("ladder");
                else if (isSnake)
                    PlaySound("snake");
            }

            // ===============================
            // 7. SIDE PORTALS
            // ===============================
            if (IsSidePortal(effect))
            {
                int targetLevel = GetSidePortalTargetLevel(effect);
                int currentSquare = square;
                if (player == 1)
                    P1SidePortSquare = currentSquare;
                else
                    P2SidePortSquare = currentSquare;
                await PlayPortalSequenceAsync(
                    "doorsOpening",
                    () => PortalTransitionAsync(player, targetLevel, currentSquare),
                    WorldAnnouncements[targetLevel],
                    "doorsClosing"
                );
                SwitchTurn();
                return;
            }

            // ===============================
            // 8. BOARD-SQUARE SOUND EFFECTS
            // ===============================
            if (IsBoardSquareSound(effect))
            {
                int index = GetBoardSquareSoundIndex(effect);
                PlaySound($"boardSound{index}");
            }


            // ===============================
            // 9. SNAKE / LADDER MOVEMENT
            // ===============================
            if (effect >= MovementEffectRangeStart && effect <= MovementEffectRangeEnd)
            {
                await Task.Delay(400);

                if (player == 1)
                    P1BoardPosition = effect;
                else
                    P2BoardPosition = effect;

                UpdateCounterPosition(player);

                // Check for win at the new position
                int newEffect = (player == 1)
                    ? currentBoardP1[P1BoardPosition]
                    : currentBoardP2[P2BoardPosition];

                if (newEffect == WinEffectCode)
                {
                    PlaySound("win");
                    await Task.Delay(2000);
                    TriggerWin(player);
                    return;
                }
            }

            // ===============================
            // 10. SWITCH TURNS
            // ===============================
            SwitchTurn();
        }


        private async Task PlayPortalSequenceAsync(
     string sound1,
     Func<Task> portalTransition,
     string sound2,
     string sound3)
        {
            // 1. First sound (doors opening)
            PlaySoundIfNotEmpty(sound1);
            await Task.Delay(2000);

            // 2. Transition
            await portalTransition();

            // 3. Second sound (world announcement)
            PlaySoundIfNotEmpty(sound2);
            if (!string.IsNullOrEmpty(sound2))
                await Task.Delay(2500);

            // 4. Third sound (doors closing)
            PlaySoundIfNotEmpty(sound3);
            if (!string.IsNullOrEmpty(sound3))
                await Task.Delay(2000);
        }

        private void PlaySoundIfNotEmpty(string soundKey)
        {
            if (!string.IsNullOrEmpty(soundKey))
                PlaySound(soundKey);
        }

        private async Task StepCounterAsync(int player, int steps)
        {
            int direction = Math.Sign(steps);     // +1 or -1
            int count = Math.Abs(steps);          // number of steps to animate

            for (int i = 0; i < count; i++)
            {
                PlayTick();

                if (player == 1)
                    P1BoardPosition += direction;
                else
                    P2BoardPosition += direction;

                UpdateCounterPosition(player);

                await Task.Delay(600);
            }
        }

        private void UpdateCounterPosition(int player)
        {
            int pos = (player == 1) ? P1BoardPosition : P2BoardPosition;
            var coords = BoardCoordinates[pos];

            if (player == 1)
            {
                Canvas.SetLeft(P1Counter, coords.X);
                Canvas.SetTop(P1Counter, coords.Y);
                Canvas.SetLeft(P1CounterButton, coords.X);
                Canvas.SetTop(P1CounterButton, coords.Y);
            }
            else
            {
                Canvas.SetLeft(P2Counter, coords.X);
                Canvas.SetTop(P2Counter, coords.Y);
                Canvas.SetLeft(P2CounterButton, coords.X);
                Canvas.SetTop(P2CounterButton, coords.Y);
            }
        }

        private string GetSquareType(int square, int effect)
        {
            // Normal squares and portal squares
            if (effect == 0 ||
                effect == FinalSquare ||                
                (effect >= SidePortalRangeStart && effect <= SidePortalRangeEnd))     // side portals
            {
                return "Normal";
            }

            // Snakes and ladders
            if (effect > square)
                return "Ladder";

            if (effect < square)
                return "Snake";

            return "Normal";
        }

        private async Task PortalTransitionAsync(int player, int targetLevel, int startSquare)
        {
            // Resolve board + image for target level
            int[] newBoard;
            string imagePath;

            if (targetLevel == 1)
            {
                newBoard = level1Board;
                imagePath = "Images/Boards/BoardLevel1.png";
            }
            else
            {
                newBoard = BoardLevels[targetLevel];
                imagePath = BoardImages[targetLevel];
            }

            if (player == 1)
            {
                P1BoardLevel = targetLevel;
                currentBoardP1 = newBoard;

                if (targetLevel == 1)
                    LeftBoardImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                else
                    LeftBoardImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                P1BoardPosition = startSquare;
                var pos = BoardCoordinates[startSquare];
                Canvas.SetLeft(P1Counter, pos.X);
                Canvas.SetTop(P1Counter, pos.Y);
                Canvas.SetLeft(P1CounterButton, pos.X);
                Canvas.SetTop(P1CounterButton, pos.Y);
            }
            else
            {
                P2BoardLevel = targetLevel;
                currentBoardP2 = newBoard;

                if (targetLevel == 1)
                    RightBoardImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                else
                    RightBoardImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                P2BoardPosition = startSquare;
                var pos = BoardCoordinates[startSquare];
                Canvas.SetLeft(P2Counter, pos.X);
                Canvas.SetTop(P2Counter, pos.Y);
                Canvas.SetLeft(P2CounterButton, pos.X);
                Canvas.SetTop(P2CounterButton, pos.Y);
            }
        }        

        private void TriggerWin(int player)
        {
            string winnerName = (player == 1) ? "Player 1" : "Player 2";

            var win = new WinWindow(winnerName);
            bool? result = win.ShowDialog();

            if (result == true)
                ResetGame();
            else
                Application.Current.Shutdown();
        }

        private void DestinationAnnouncement(int targetLevel)
        {
            if (WorldAnnouncements.TryGetValue(targetLevel, out string clip))
                PlaySound(clip);
        }

    }
}