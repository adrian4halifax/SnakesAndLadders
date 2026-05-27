using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnakesAndLadders
{
    public partial class MainWindow
    {
        //Core game state variables
        public int P1BoardPosition = 0;
        private int P2BoardPosition = 0;

        private int P1BoardLevel = 1;
        private int P2BoardLevel = 1;

        public string Player1CurrentBoardName { get; set; } = "1";
        public string Player2CurrentBoardName { get; set; } = "1";

        public bool InfiniteTokensEnabled { get; set; }
        public int Player1Tokens { get; set; } = 3;
        public int Player2Tokens { get; set; } = 3;

        public int DefaultReturnSquare { get; set; } = 1;


        //Player token state
        private bool Player1HasTokens = true;
        private bool Player2HasTokens = true;
        public enum TokenMode
        {
            None,
            Three,
            Infinite
        }
        public TokenMode CurrentTokenMode { get; set; } = TokenMode.Three;


        //Board data
        internal Dictionary<int, int[]> BoardLevels;
        internal Dictionary<int, string> BoardImages;

        //public List<string> BoardNames { get; private set; }

        private int[] level1Board = new int[101];
        private int[] level2Board = new int[101];
        private int[] level3Board = new int[101];
        private int[] level4Board = new int[101];
        private int[] level5Board = new int[101];
        private int[] level6Board = new int[101];
        private int[] level7Board = new int[101];
        private int[] level8Board = new int[101];

        private int[] currentBoardP1;
        private int[] currentBoardP2;

        //Board and effect constants        
        private const int FinalSquare = 100;
        private const int SidePortalRangeStart = 401;
        private const int SidePortalRangeEnd = 499;
        private const int BoardSquareSoundStart = 601;
        private const int BoardSquareSoundEnd = 699;       
        private const int MovementEffectRangeStart = 1;
        private const int MovementEffectRangeEnd = 100;

        
        private int GetBoardSquareSoundIndex(int effect) =>
            effect - BoardSquareSoundStart + 1;   // 601→1, 602→2, … 699→99

        private bool IsSidePortal(int effect) =>
            effect >= SidePortalRangeStart && effect <= SidePortalRangeEnd;

       
        private bool IsBoardSquareSound(int effect) =>
            effect >= BoardSquareSoundStart && effect <= BoardSquareSoundEnd;
       
        private int GetSidePortalTargetLevel(int effect) =>
            effect - SidePortalRangeStart + 1;

        private const int WinEffectCode = 1000;


        //Portal state        
        int P1SidePortSquare;
        int P2SidePortSquare;

        //Board coordinate system
        private Point[] BoardCoordinates = new Point[101];

        //Die and RNG
        private int roll;
        private int _lastDieRoll;
        private int _currentPlayer = 1;   // 1 = Player 1, 2 = Player 2

        private Random _rng = new Random();

        //Audio system
        private Dictionary<string, MediaPlayer> soundCache = new Dictionary<string, MediaPlayer>();
        private Dictionary<int, string> WorldAnnouncements;
        private Dictionary<int, string> EffectSoundMap;
        public static Dictionary<int, string> BoardNames;
        private Dictionary<string, Uri> soundUris = new Dictionary<string, Uri>();

        private readonly SoundPlayer tickPlayer = new SoundPlayer("Sounds/GamePlaySounds/Tick.wav");
        private readonly MediaPlayer sfxPlayer = new MediaPlayer();


    }
}
