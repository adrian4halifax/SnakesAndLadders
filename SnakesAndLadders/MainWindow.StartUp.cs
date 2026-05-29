using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnakesAndLadders
{
    public partial class MainWindow
    {
        private void InitialiseTurnState()
        {
            // Player 1 starts
            P1TurnCounter.Opacity = 1.0;
            P2TurnCounter.Opacity = 0.4;

            P1GoButton.Visibility = Visibility.Visible;
            P2GoButton.Visibility = Visibility.Collapsed;

            _currentPlayer = 1;
        }

        private void InitialiseBoardCoordinates()
        {
            int squareSize = 58;
            
            // Centre of square 100
            double baseX = 10;
            double baseY = 10;

            for (int i = 1; i <= 100; i++)
            {
                int row = (i - 1) / 10;     // 0–9
                int col = (i - 1) % 10;     // 0–9

                // Reverse direction every other row
                if (row % 2 == 1)
                    col = 9 - col;

                double x = baseX + (col * squareSize);
                double y = baseY + ((9 - row) * squareSize);

                BoardCoordinates[i] = new Point(x, y);
            }
        }
        

private int[] LoadBoardActions(string filename)
{
    try
    {
        var lines = File.ReadAllLines(filename);
        int[] board = new int[101];
        for (int i = 1; i <= 100; i++)
        {
            board[i] = int.Parse(lines[i - 1]);
        }
        return board;
    }
    catch (IOException ex)
    {
        MessageBox.Show($"Error loading board file: {filename}\n{ex.Message}", "File Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return new int[101];
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Unexpected error loading board file: {filename}\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return new int[101];
    }
}

private void LoadBoardActionsFiles()
{
    level1Board = LoadBoardActions("Data/BL1_Actions.csv");
    level2Board = LoadBoardActions("Data/BL2_Actions.csv");
    level3Board = LoadBoardActions("Data/BL3_Actions.csv");
    level4Board = LoadBoardActions("Data/BL4_Actions.csv");
    level5Board = LoadBoardActions("Data/BL5_Actions.csv");
    level6Board = LoadBoardActions("Data/BL6_Actions.csv");
    level7Board = LoadBoardActions("Data/BL7_Actions.csv");
    level8Board = LoadBoardActions("Data/BL8_Actions.csv");

            BoardLevels = new Dictionary<int, int[]>
    {
        { 1, level1Board },
        { 2, level2Board },
        { 3, level3Board },
        { 4, level4Board },
        { 5, level5Board },
        { 6, level6Board },
        { 7, level7Board },
        { 8, level8Board }
    };
}     

        private void LoadSoundLibrary()
        {
            AddSoundToLibrary("die", "Sounds/GamePlaySounds/DieRoll.wav");
            AddSoundToLibrary("ladder", "Sounds/GamePlaySounds/Ladder.wav");
            AddSoundToLibrary("snake", "Sounds/GamePlaySounds/Snake.flac");
            AddSoundToLibrary("win", "Sounds/GamePlaySounds/Win.wav");           

            AddSoundToLibrary("doorsOpening", "Sounds/Worlds/DoorsOpening.mp3");
            AddSoundToLibrary("doorsClosing", "Sounds/Worlds/DoorsClosing.mp3");
            AddSoundToLibrary("chaos", "Sounds/Worlds/Chaos.mp3");
            AddSoundToLibrary("prehistoricWorld", "Sounds/Worlds/PrehistoricWorld.mp3");
            AddSoundToLibrary("spidermansWorld", "Sounds/Worlds/SpidermansWorld.mp3");
            AddSoundToLibrary("spaceWorld", "Sounds/Worlds/SpaceWorld.mp3");
            AddSoundToLibrary("snakePit", "Sounds/Worlds/SnakePit.mp3");
            AddSoundToLibrary("monsterTruck", "Sounds/Worlds/MonsterTruck.mp3");
            AddSoundToLibrary("tradSnakesAndLadders", "Sounds/Worlds/TradSnakesAndLadders.mp3");
            AddSoundToLibrary("upsideDownWorld", "Sounds/Worlds/UpsideDownWorld.mp3");

            AddSoundToLibrary("boardSound38", "Sounds/MonsterTruckSounds/Monster1.mp3");
            AddSoundToLibrary("boardSound39", "Sounds/MonsterTruckSounds/Monster2.mp3");
            AddSoundToLibrary("boardSound40", "Sounds/MonsterTruckSounds/Monster3.wav");
            AddSoundToLibrary("boardSound41", "Sounds/MonsterTruckSounds/Monster4.wav");

            AddSoundToLibrary("boardSound1", "Sounds/ChaosSounds/Chaos1.mp3");
            AddSoundToLibrary("boardSound2", "Sounds/ChaosSounds/Chaos2.mp3");
            AddSoundToLibrary("boardSound3", "Sounds/ChaosSounds/Chaos3.mp3");   
            AddSoundToLibrary("boardSound4", "Sounds/ChaosSounds/Chaos4.mp3");   
            AddSoundToLibrary("boardSound5", "Sounds/ChaosSounds/Chaos5.mp3");
            AddSoundToLibrary("boardSound6", "Sounds/ChaosSounds/Chaos6.mp3");
            AddSoundToLibrary("boardSound7", "Sounds/ChaosSounds/Chaos7.mp3");

            AddSoundToLibrary("boardSound12", "Sounds/ChaosSounds/Chaos8.mp3");
            AddSoundToLibrary("boardSound13", "Sounds/ChaosSounds/Chaos9.mp3");
            AddSoundToLibrary("boardSound14", "Sounds/ChaosSounds/Chaos10.mp3");
            AddSoundToLibrary("boardSound15", "Sounds/ChaosSounds/Chaos11.mp3");
            AddSoundToLibrary("boardSound16", "Sounds/ChaosSounds/Chaos12.mp3");
            AddSoundToLibrary("boardSound17", "Sounds/ChaosSounds/Chaos13.mp3");
            AddSoundToLibrary("boardSound18", "Sounds/ChaosSounds/Chaos14.mp3");
            AddSoundToLibrary("boardSound19", "Sounds/ChaosSounds/Chaos15.mp3");
            AddSoundToLibrary("boardSound20", "Sounds/ChaosSounds/Chaos16.mp3");
            AddSoundToLibrary("boardSound21", "Sounds/ChaosSounds/Chaos17.mp3");
            AddSoundToLibrary("boardSound22", "Sounds/ChaosSounds/Chaos18.mp3");
            AddSoundToLibrary("boardSound23", "Sounds/ChaosSounds/Chaos19.mp3");
            AddSoundToLibrary("boardSound24", "Sounds/ChaosSounds/Chaos20.mp3");
            AddSoundToLibrary("boardSound25", "Sounds/ChaosSounds/Chaos21.mp3");
            AddSoundToLibrary("boardSound26", "Sounds/ChaosSounds/Chaos22.mp3");
            AddSoundToLibrary("boardSound27", "Sounds/ChaosSounds/Chaos23.mp3");
            AddSoundToLibrary("boardSound28", "Sounds/ChaosSounds/Chaos24.mp3");
            AddSoundToLibrary("boardSound29", "Sounds/ChaosSounds/Chaos25.mp3");
            AddSoundToLibrary("boardSound30", "Sounds/ChaosSounds/Chaos26.mp3");
            AddSoundToLibrary("boardSound31", "Sounds/ChaosSounds/Chaos27.mp3");
            AddSoundToLibrary("boardSound32", "Sounds/ChaosSounds/Chaos28.mp3");
            AddSoundToLibrary("boardSound33", "Sounds/ChaosSounds/Chaos29.mp3");
            AddSoundToLibrary("boardSound34", "Sounds/ChaosSounds/Chaos30.mp3");
            AddSoundToLibrary("boardSound35", "Sounds/ChaosSounds/Chaos31.mp3");
            AddSoundToLibrary("boardSound36", "Sounds/ChaosSounds/Chaos32.mp3");
            AddSoundToLibrary("boardSound37", "Sounds/ChaosSounds/Chaos33.mp3");
            AddSoundToLibrary("boardSound42", "Sounds/ChaosSounds/Chaos34.wav");
            AddSoundToLibrary("boardSound43", "Sounds/ChaosSounds/Chaos35.wav");
            AddSoundToLibrary("boardSound44", "Sounds/ChaosSounds/Chaos36.wav");
            AddSoundToLibrary("boardSound45", "Sounds/ChaosSounds/Chaos37.mp3");
            AddSoundToLibrary("boardSound46", "Sounds/ChaosSounds/Chaos38.wav");
            AddSoundToLibrary("boardSound47", "Sounds/ChaosSounds/Chaos39.wav");
            AddSoundToLibrary("boardSound48", "Sounds/ChaosSounds/Chaos40.wav");
            AddSoundToLibrary("boardSound49", "Sounds/ChaosSounds/Chaos41.wav");
            AddSoundToLibrary("boardSound50", "Sounds/ChaosSounds/Chaos42.wav");
            AddSoundToLibrary("boardSound51", "Sounds/ChaosSounds/Chaos43.wav");
            AddSoundToLibrary("boardSound52", "Sounds/ChaosSounds/Chaos44.wav");
            AddSoundToLibrary("boardSound53", "Sounds/ChaosSounds/Chaos45.wav");
            AddSoundToLibrary("boardSound54", "Sounds/ChaosSounds/Chaos46.mp3");
            AddSoundToLibrary("boardSound55", "Sounds/ChaosSounds/Chaos47.wav");
            AddSoundToLibrary("boardSound56", "Sounds/ChaosSounds/Chaos48.wav");
            AddSoundToLibrary("boardSound57", "Sounds/ChaosSounds/Chaos49.wav");
            AddSoundToLibrary("boardSound58", "Sounds/ChaosSounds/Chaos50.wav");
            AddSoundToLibrary("boardSound59", "Sounds/ChaosSounds/Chaos51.wav");
            AddSoundToLibrary("boardSound60", "Sounds/ChaosSounds/Chaos52.wav");
            AddSoundToLibrary("boardSound61", "Sounds/ChaosSounds/Chaos53.wav");
            AddSoundToLibrary("boardSound62", "Sounds/ChaosSounds/Chaos54.wav");
            AddSoundToLibrary("boardSound63", "Sounds/ChaosSounds/Chaos55.wav");
            AddSoundToLibrary("boardSound64", "Sounds/ChaosSounds/Chaos56.wav");
            AddSoundToLibrary("boardSound65", "Sounds/ChaosSounds/Chaos57.wav");
            AddSoundToLibrary("boardSound66", "Sounds/ChaosSounds/Chaos58.flac");
            AddSoundToLibrary("boardSound67", "Sounds/ChaosSounds/Chaos59.wav");
            AddSoundToLibrary("boardSound68", "Sounds/ChaosSounds/Chaos60.wav");

            AddSoundToLibrary("boardSound8", "Sounds/PrehistoricSounds/Prehistoric1.mp3");

            AddSoundToLibrary("boardSound9", "Sounds/UpsideDownSounds/UpsideDown1.mp3");
            AddSoundToLibrary("boardSound10", "Sounds/UpsideDownSounds/UpsideDown2.mp3");
            AddSoundToLibrary("boardSound11", "Sounds/UpsideDownSounds/UpsideDown3.mp3");

            tickPlayer.Load(); //Loads SoundPlayer into memory for counter stepping ticks
        }

        private void AddSoundToLibrary(string key, string relativePath)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(basePath, relativePath);
            var uri = new Uri(fullPath, UriKind.Absolute);
            soundUris[key] = uri;
        }


        private void LoadSoundEffects()
        {
            EffectSoundMap = new Dictionary<int, string>
            {   
                //Board sound effects 601–606
                { 601, "boardSound1" },
                { 602, "boardSound2" },
                { 603, "boardSound3" },
                { 604, "boardSound4" },
                { 605, "boardSound5" },
                { 606, "boardSound6" },
                { 607, "boardSound7" },
                { 608, "boardSound8" },
                { 609, "boardSound9" },
                { 610, "boardSound10" },
                { 611, "boardSound11" },
                { 612, "boardSound12" },
                { 613, "boardSound13" },
                { 614, "boardSound14" },
                { 615, "boardSound15" },
                { 616, "boardSound16" },
                { 617, "boardSound17" },
                { 618, "boardSound18" },
                { 619, "boardSound19" },
                { 620, "boardSound20" },
                { 621, "boardSound21" },
                { 622, "boardSound22" },
                { 623, "boardSound23" },
                { 624, "boardSound24" },
                { 625, "boardSound25" },
                { 626, "boardSound26" },
                { 627, "boardSound27" },
                { 628, "boardSound28" },
                { 629, "boardSound29" },
                { 630, "boardSound30" },
                { 631, "boardSound31" },
                { 632, "boardSound32" },
                { 633, "boardSound33" },
                { 634, "boardSound34" },
                { 635, "boardSound35" },
                { 636, "boardSound36" },
                { 637, "boardSound37" },
                { 638, "boardSound38" },
                { 639, "boardSound39" },
                { 640, "boardSound40" },
                { 641, "boardSound41" },
                { 642, "boardSound42" },
                { 643, "boardSound43" },
                { 644, "boardSound44" },
                { 645, "boardSound45" },
                { 646, "boardSound46" },
                { 647, "boardSound47" },
                { 648, "boardSound48" },
                { 649, "boardSound49" },
                { 650, "boardSound50" },
                { 651, "boardSound51" },
                { 652, "boardSound52" },
                { 653, "boardSound53" },
                { 654, "boardSound54" },
                { 655, "boardSound55" },
                { 656, "boardSound56" },
                { 657, "boardSound57" },
                { 658, "boardSound58" },
                { 659, "boardSound59" },
                { 660, "boardSound60" },
                { 661, "boardSound61" },
                { 662, "boardSound62" },
                { 663, "boardSound63" },
                { 664, "boardSound64" },
                { 665, "boardSound65" },
                { 666, "boardSound66" },
                { 667, "boardSound67" },
                { 668, "boardSound68" }

            };
        }


        private void LoadWorldAnnouncements()
        {
            WorldAnnouncements = new Dictionary<int, string>
    {
        { 1, "tradSnakesAndLadders" },
        { 2, "snakePit" },
        { 3, "spaceWorld" },
        { 4, "prehistoricWorld" },
        { 5, "spidermansWorld" },
        { 6, "monsterTruck" },
        { 7, "chaos" },
        { 8, "upsideDownWorld" }
    };

        }


        private async Task PreloadSoundsAsync()
        {
            foreach (var uri in soundUris.Values)   // soundUris = your dictionary of URIs
            {
                var temp = new MediaPlayer();
                temp.Open(uri);

                // Wait for MediaOpened
                var tcs = new TaskCompletionSource<bool>();
                temp.MediaOpened += (s, e) => tcs.TrySetResult(true);

                await tcs.Task;

                // Close immediately — hydration complete
                temp.Close();
            }
        }



        private void PlaySound(string key)
        {
            if (soundUris.TryGetValue(key, out var uri))
            {
                sfxPlayer.Stop();
                sfxPlayer.Open(uri);
                sfxPlayer.Play();
            }
        }


        private void PlayTick()
        {
            tickPlayer.Play();
        }

        private void InitialiseBoardImages()
        {
            BoardImages = new Dictionary<int, string>
            {
                { 1, "Images/Boards/BoardLevel1.png" },
                { 2, "Images/Boards/BoardLevel2.png" },
                { 3, "Images/Boards/BoardLevel3.png" },
                { 4, "Images/Boards/BoardLevel4.png" },
                { 5, "Images/Boards/BoardLevel5.png" },
                { 6, "Images/Boards/BoardLevel6.png" },
                { 7, "Images/Boards/BoardLevel7.png" },
                { 8, "Images/Boards/BoardLevel8.png" }
            };
        }

        private void InitialiseBoardNames()
        {
            BoardNames = new Dictionary<int, string>
    {
        { 1, "Traditional Snakes & Ladders" },
        { 2, "Snake Pit" },
        { 3, "Space World" },
        { 4, "Prehistoric World" },
        { 5, "Spiderman's World" },
        { 6, "Monster Truck" },
        { 7, "Chaos Zone" },
        { 8, "Upside Down World" }
    };
        }

        private void InitialiseBoardLevels()
        {
            BoardLevels = new Dictionary<int, int[]>
            {
                { 1, level1Board },
                { 2, level2Board },
                { 3, level3Board },
                { 4, level4Board },
                { 5, level5Board },
                { 6, level6Board },
                { 7, level7Board },
                { 8, level8Board }
            };
        }
       private void SetBoard()
       {
            currentBoardP1 = level1Board;
            currentBoardP2 = level1Board;
       }

    }
}