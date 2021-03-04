using System;
using System.Collections.Generic;
using System.Timers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace FinalProject_Game
{
    public class Board
    {
        #region class properties
        public double PlayerStep = 15;
        public Canvas BoardCanvas;
        public Player Player;
        public List<Enemy> Enemies;
        public List<Pokeballs> Poke;
        public DispatcherTimer Timer;       
        
        public Canvas Secondboard;
        public Canvas ControlsCanvas;

        public string direction = "";
        public int ScoreValue = 0;
        public int Size = 0;
        public int WaveNum = 1;
        public int Limit = 40;
        public int ElapsedSec;

        public Button TryAgain;
        public Button Startbtn;
        public Button Menubtn;
        public Button Exitbtn;
        public Button Exit2;
        public Button Control;
        public Button BackFromControl;

        public TextBlock TimeSurvivedBlk;
        public TextBlock Score;
        public TextBlock WelcomeMsg;
        public TextBlock Nonselect;
        public TextBlock ScoreBoard;
        public TextBlock Waveblk;

        public ComboBox Diffculty;
        public Image Charmander, Pikatchu , GameOverImg;             
        public Sounds Sounds;
        #endregion
        public Board(Canvas MainCanvas, Image image, Button exit, Canvas canvas, Button exit1, Canvas controls)
        {
            Sounds = new Sounds();
            Timer = new DispatcherTimer();
            ElapsedSec = 0;
           
            //time counter---------------------------------------------------------
            var timer2 = new Timer(1000);
            timer2.Elapsed += (Object source, ElapsedEventArgs e) => ElapsedSec++;
            timer2.AutoReset = true;
            timer2.Enabled = true;
            
            Exit2 = exit1;
            Secondboard = canvas;
            ControlsCanvas = controls;
            Exitbtn = exit;
            GameOverImg = image;
            BoardCanvas = MainCanvas;
            UserInterface();         
        }
       
        #region User interface 
        private void UserInterface()
        {
            // start Button
            Startbtn = UIElementsFactory.CreateStartBtn();
            Startbtn.Click += Startbtn_Click;
            Startbtn.PointerEntered += Startbtn_PointerEntered;
            Startbtn.PointerExited += Startbtn_PointerExited;
            Secondboard.Children.Add(Startbtn);
            Canvas.SetTop(Startbtn, 470);
            Canvas.SetLeft(Startbtn, 780);

            // Tryagain Button
            TryAgain = UIElementsFactory.CreatTryAgainBtn();
            TryAgain.Click += TryAgain_Click; ; ;
            TryAgain.PointerMoved += TryAgain_PointerMoved;
            TryAgain.PointerExited += TryAgain_PointerExited;
            BoardCanvas.Children.Add(TryAgain);
            Canvas.SetTop(TryAgain, 540);
            Canvas.SetLeft(TryAgain, 870);
            Canvas.SetZIndex(TryAgain, 4);

            // Time survived txt block
            TimeSurvivedBlk = UIElementsFactory.TimeSurvivedTXTblock();
            Canvas.SetTop(TimeSurvivedBlk, 5);
            Canvas.SetLeft(TimeSurvivedBlk, 20);
            Canvas.SetZIndex(TimeSurvivedBlk, 4);
            BoardCanvas.Children.Add(TimeSurvivedBlk);

            //score board txt block
            Score = UIElementsFactory.ScoreBoardTimeTXTblock();
            Canvas.SetTop(Score, 320);
            Canvas.SetLeft(Score, 680);
            BoardCanvas.Children.Add(Score);

            //welcome messege
            WelcomeMsg = UIElementsFactory.WelcomeMsg();
            Canvas.SetTop(WelcomeMsg, 20);
            Canvas.SetLeft(WelcomeMsg, 350);
            Secondboard.Children.Add(WelcomeMsg);

            //diffcult level
            Diffculty = UIElementsFactory.SelectDiffcultyCB();
            Canvas.SetTop(Diffculty, 660);
            Canvas.SetLeft(Diffculty, 830);
            Secondboard.Children.Add(Diffculty);
            Diffculty.SelectionChanged += Diffculty_SelectionChanged;

            //adding char+pika to menu
            Charmander = UIElementsFactory.AddCharmander();
            Canvas.SetTop(Charmander, 200);
            Canvas.SetLeft(Charmander, 1400);
            Secondboard.Children.Add(Charmander);

            Pikatchu = UIElementsFactory.AddPikactchu();
            Canvas.SetTop(Pikatchu, 200);
            Canvas.SetLeft(Pikatchu, 100);
            Secondboard.Children.Add(Pikatchu);

            //adding error block to combo
            Nonselect = UIElementsFactory.ErorrNonSelectedBLK();
            Canvas.SetTop(Nonselect, 700);
            Canvas.SetLeft(Nonselect, 770);
            Secondboard.Children.Add(Nonselect);

            //adding menu button
            Menubtn = UIElementsFactory.CreateMenuBTN();
            Canvas.SetTop(Menubtn, 650);
            Canvas.SetLeft(Menubtn, 870);
            Canvas.SetZIndex(Menubtn, 2);
            BoardCanvas.Children.Add(Menubtn);
            Menubtn.Click += Menubtn_Click;
            Menubtn.PointerMoved += Menubtn_PointerMoved;
            Menubtn.PointerExited += Menubtn_PointerExited;

            //adding score board
            ScoreBoard = UIElementsFactory.CreateScoreBoard();
            Canvas.SetTop(ScoreBoard, 8);
            Canvas.SetLeft(ScoreBoard, 600);
            BoardCanvas.Children.Add(ScoreBoard);
            ScoreBoard.Visibility = Visibility.Visible;

            //adding wave block
            Waveblk = UIElementsFactory.CreateScoreBoard();
            Canvas.SetTop(Waveblk, 8);
            Canvas.SetLeft(Waveblk, 950);
            BoardCanvas.Children.Add(Waveblk);
            Waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            Waveblk.Text = $"Wave: {WaveNum++}      Enemies: {Size}";
            Waveblk.Visibility = Visibility.Visible;
            Waveblk.Width = 700;

            //adding controls btn
            Control = UIElementsFactory.CreateStartBtn();
            Control.Content = "Controls";
            Control.Click += Control_Click;
            Canvas.SetTop(Control, 560);
            Canvas.SetLeft(Control, 780);
            Secondboard.Children.Add(Control);

            //adding BACK from controls btn
            BackFromControl = UIElementsFactory.CreateMenuBTN();
            BackFromControl.Click += BackFromControl_Click;
            Canvas.SetTop(BackFromControl, 940);
            Canvas.SetLeft(BackFromControl, 0);
            BackFromControl.Visibility = Visibility.Collapsed;
            BackFromControl.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            BackFromControl.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
            ControlsCanvas.Children.Add(BackFromControl);
        }
        public void WaveColorChange()
        {
           if (WaveNum <= 5) Waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
           if (WaveNum > 5 && WaveNum <= 10) Waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Aqua);
           if (WaveNum > 10 && WaveNum <= 15) Waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Yellow);
           if (WaveNum > 15 && WaveNum <= 20) Waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Orange);
           if (WaveNum > 20 && WaveNum <= 25) Waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);       
        }
        #endregion

        #region click events
        private void BackFromControl_Click(object sender, RoutedEventArgs e)
        {

            Secondboard.Visibility = Visibility.Visible;
            ControlsCanvas.Visibility = Visibility.Collapsed;
            BackFromControl.Visibility = Visibility.Collapsed;
        }
        private void Control_Click(object sender, RoutedEventArgs e)
        {
            Secondboard.Visibility = Visibility.Collapsed;
            ControlsCanvas.Visibility = Visibility.Visible;
            BackFromControl.Visibility = Visibility.Visible;
        }
        private void Menubtn_Click(object sender, RoutedEventArgs e)
        {
            BoardCanvas.Visibility = Visibility.Collapsed;
            Secondboard.Visibility = Visibility.Visible;
            Menubtn.Visibility = Visibility.Collapsed;
            TryAgain.Visibility = Visibility.Collapsed;
            Exitbtn.Visibility = Visibility.Collapsed;
            Exit2.Visibility = Visibility.Collapsed;
            GameOverImg.Visibility = Visibility.Collapsed;
            Score.Visibility = Visibility.Collapsed;
            TimeSurvivedBlk.Visibility = Visibility.Visible;
            ScoreBoard.Text = "Score:0 ";
            ScoreBoard.FontSize = 50;
            Canvas.SetTop(ScoreBoard, 8);
            Waveblk.Text = $"Wave: {WaveNum = 1}      Enemies: {Size}";
            Waveblk.FontSize = 50;
            Canvas.SetTop(Waveblk, 8);
            Limit = 40;

            ScoreValue = 0;
            ElapsedSec = 0;
            CreateEnemies(Size);
            CreatePlayer();
            CreatePokeBall();
            ClearBoard();

        }
        private void TryAgain_Click(object sender, RoutedEventArgs e)
        {

            TryAgain.Visibility = Visibility.Collapsed;
            Exitbtn.Visibility = Visibility.Collapsed;
            Exit2.Visibility = Visibility.Collapsed;
            GameOverImg.Visibility = Visibility.Collapsed;
            Score.Visibility = Visibility.Collapsed;
            TimeSurvivedBlk.Visibility = Visibility.Visible;
            Menubtn.Visibility = Visibility.Collapsed;
            Secondboard.Visibility = Visibility.Collapsed;
            ScoreBoard.Visibility = Visibility.Visible;
            Waveblk.Visibility = Visibility.Visible;
            ScoreBoard.Text = "Score:0 ";
            ScoreBoard.FontSize = 50;
            Canvas.SetTop(ScoreBoard, 8);
            Waveblk.Text = $"Wave: {WaveNum = 1}      Enemies: {Size}";
            Waveblk.FontSize = 50;
            Canvas.SetTop(Waveblk, 8);
            Limit = 40;

            ElapsedSec = 0;
            ScoreValue = 0;
            CreatePlayer();
            CreatePokeBall();
            CreateEnemies(Size);


            Timer.Start();
        }
        public void Startbtn_Click(object sender, RoutedEventArgs e)
        {

            if (Diffculty.SelectedItem == null)
            {
                Nonselect.Text = $"Please selcet difficulty!";
            }
            else
            {
                Timer.Start();
                Nonselect.Visibility = Visibility.Collapsed;
                BoardCanvas.Visibility = Visibility.Visible;
                TryAgain.Visibility = Visibility.Collapsed;
                Exitbtn.Visibility = Visibility.Visible;
                Exit2.Visibility = Visibility.Collapsed;
                GameOverImg.Visibility = Visibility.Collapsed;
                Score.Visibility = Visibility.Collapsed;
                TimeSurvivedBlk.Visibility = Visibility.Visible;
                Menubtn.Visibility = Visibility.Collapsed;
                Secondboard.Visibility = Visibility.Collapsed;
                ScoreBoard.Visibility = Visibility.Visible;
                ScoreBoard.Text = "Score:0 ";
                ScoreBoard.FontSize = 50;
                Canvas.SetTop(ScoreBoard, 8);
                Waveblk.Text = $"Wave: {WaveNum = 1}      Enemies: {Size}";
                Waveblk.FontSize = 50;
                Canvas.SetTop(Waveblk, 8);
                Limit = 40;

                ElapsedSec = 0;
                ScoreValue = 0;
                CreateEnemies(Size);
                CreatePlayer();
                CreatePokeBall();
            }
        }
        #endregion

        #region diffculty level
        public void Diffculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Diffculty.SelectedItem.ToString() == "Expert")
            {
                Size = 4;
            }
            else if (Diffculty.SelectedItem.ToString() == "Hard")
            {
                Size = 3;
            }
            else if (Diffculty.SelectedItem.ToString() == "Medium")
            {
                Size = 2;
            }
            else if (Diffculty.SelectedItem.ToString() == "Easy")
            {
                Size = 1;
            }
            ElapsedSec = 0;
        }

        #endregion

        #region create funnctions
        public void CreatePokeBall()
        {
            Random random = new Random();
            Poke = new List<Pokeballs>();
            for (int i = 0; i < 1; i++)
            {
                Pokeballs pokeballs = new Pokeballs();
                pokeballs.Y = random.Next(30, 930);
                pokeballs.X = random.Next(10, 1800);
                pokeballs.Height = 70;
                pokeballs.Width = 70;
                BoardCanvas.Children.Add(pokeballs.Element);
                Poke.Add(pokeballs);
            }
        }
        public void CreatePlayer()
        {
            Player = new Player();
            BoardCanvas.Children.Add(Player.Element);
            Player.Z = 4;
            Player.X = 850;
            Player.Y = 0;
        }
        public void CreateEnemies(int amount)
        {
            Random random = new Random();
            Enemies = new List<Enemy>();

            for (int i = 0; i < amount; i++)
            {
                Enemy e = new Enemy();
                e.X = random.Next(0, 2400);
                e.Y = random.Next(600, 1550);
                BoardCanvas.Children.Add(e.Element);
                Enemies.Add(e);
            }
        }
        #endregion

        # region pointers events
        private void Startbtn_PointerExited(object sender, PointerRoutedEventArgs e) 
        {
            Startbtn.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
        }
        private void Startbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Startbtn.Background = new SolidColorBrush(Windows.UI.Colors.IndianRed);
        }
        private void TryAgain_PointerExited(object sender, PointerRoutedEventArgs e) 
        {
            TryAgain.Background = new SolidColorBrush(Windows.UI.Colors.Red);
        }
        private void TryAgain_PointerMoved(object sender, PointerRoutedEventArgs e)  
        {
            TryAgain.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }
        private void Menubtn_PointerExited(object sender, PointerRoutedEventArgs e)  
        {
            Menubtn.Background = new SolidColorBrush(Windows.UI.Colors.Red);
        }
        private void Menubtn_PointerMoved(object sender, PointerRoutedEventArgs e)   
        {
            Menubtn.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }
        #endregion

        # region clear board function
        public void ClearBoard()
        {
            foreach (var ene in Enemies)
            {
                BoardCanvas.Children.Remove(ene.Element);
            }
            foreach (var pok in Poke)
            {
                BoardCanvas.Children.Remove(pok.Element);
            }
            BoardCanvas.Children.Remove(Player.Element);
        }
        #endregion
    }
}
