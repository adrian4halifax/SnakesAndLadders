using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static SnakesAndLadders.MainWindow;

namespace SnakesAndLadders
{
    public partial class PreferencesWindow : Window
    {
        private string _p1SelectedColor = "Red";
        private string _p2SelectedColor = "Green";


        private readonly MainWindow _main;

        public PreferencesWindow(MainWindow main)
        {
            InitializeComponent();
            _main = main;
            _main.ResetForPreferences();

            LoadBoardCombo();
            UpdateCounterSelectionUI(1, _p1SelectedColor);
            UpdateCounterSelectionUI(2, _p2SelectedColor);
        }


        

        private void LoadBoardCombo()
        {
            BoardCombo.ItemsSource = MainWindow.BoardNames
                .Select(kvp => new { Id = kvp.Key, Name = kvp.Value });

            BoardCombo.DisplayMemberPath = "Name";
            BoardCombo.SelectedValuePath = "Id";

            // Default selection
            BoardCombo.SelectedValue = 1;
        }


        private void CounterColor_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img && img.Tag is string color)
            {
                if (img.Name.StartsWith("P1"))
                {
                    _p1SelectedColor = color;
                    UpdateCounterSelectionUI(1, color);
                }
                else if (img.Name.StartsWith("P2"))
                {
                    _p2SelectedColor = color;
                    UpdateCounterSelectionUI(2, color);
                }
            }
        }

        private void UpdateCounterSelectionUI(int player, string selectedColor)
        {
            for (int i = 0; i < 6; i++)
            {
                string[] colors = { "Red", "Orange", "Yellow", "Green", "Blue", "Purple" };
                string imgName = $"P{player}Counter{colors[i]}";
                var img = (Image)this.FindName(imgName);
                if (img != null)
                {
                    img.Opacity = (colors[i] == selectedColor) ? 1.0 : 0.4;
                }
            }

            // Update Go button preview image
            string goImgName = player == 1 ? "P1GoButtonImage" : "P2GoButtonImage";
            var goImg = (Image)this.FindName(goImgName);
            if (goImg != null)
            {
                goImg.Source = new BitmapImage(
                    new Uri($"Images/Counters/Counter{selectedColor}.png", UriKind.Relative));
            }

            // ⭐ Update Go text colour preview
            UpdateGoTextColor(player, selectedColor);
        }

        private void UpdateGoTextColor(int player, string colorName)
        {
            bool pale = colorName == "Yellow" || colorName == "Orange";

            var preview = player == 1 ? P1GoButtonPreview : P2GoButtonPreview;

            preview.Foreground = pale ? Brushes.Black : Brushes.White;
        }


        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // BOARD SELECTION
            if (BoardCombo.SelectedValue is int boardId)
            {
                // Apply to both players for now
                _main.SetPlayerBoard(1, boardId.ToString());
                _main.SetPlayerBoard(2, boardId.ToString());
            }

            // COUNTER COLOUR SELECTION
            _main.ApplyCounterColours(_p1SelectedColor, _p2SelectedColor);

            // TOKEN SETTINGS SELECTION
            TokenMode selectedMode =
    Tokens0.IsChecked == true ? TokenMode.None :
    TokensUnlimited.IsChecked == true ? TokenMode.Infinite :
    TokenMode.Three;
            _main.ApplyTokenSettings(selectedMode);


            this.DialogResult = true;
            this.Close();
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
