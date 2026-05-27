using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakesAndLadders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();
            InitialiseTurnState();
            InitialiseBoardCoordinates();
            LoadBoardActionsFiles();
            LoadSoundLibrary();
            LoadSoundEffects();
            LoadWorldAnnouncements();
            InitialiseBoardImages();
            InitialiseBoardLevels();
            InitialiseBoardNames();           
            SetBoard();            
        }

        
    }
}
