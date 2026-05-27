using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SnakesAndLadders
{
    public partial class MainWindow
    {
        private void MenuRules_Click(object sender, RoutedEventArgs e)
        {
            var rules = new RulesWindow();
            rules.Owner = this;
            rules.ShowDialog();
        }

        private void MenuRestart_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }       

        private void MenuPreferences_Click(object sender, RoutedEventArgs e)
        {
            var prefs = new PreferencesWindow(this);
            bool? result = prefs.ShowDialog();

            if (result == true)
            {
                // Preferences applied successfully
                RefreshPreferencesAppliedState();
            }
        }

    }
}
