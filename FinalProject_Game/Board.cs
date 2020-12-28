using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace FinalProject_Game
{
    public class Board
    {
        double playerStep = 15;
        private Canvas boardCanvas;
        private Player player;
        public List<Enemy> enemies;
        public List<Pokeballs> poke;
        public

        bool moveUp, moveDown, moveLeft, moveRight;
        private DispatcherTimer timer = new DispatcherTimer();

        MediaPlayer songPlayer;
        bool playing;

        public string direction = "";
        public Canvas secondboard;
        public int scoreValue = 0;
        public int size = 0;
        public int waveNum = 1;
        int limit = 40;

        Button tryAgain;
        Button startbtn;
        Button Menubtn;
        Button gameOver;
        Button exit2;

        TextBlock surv;
        TextBlock score;
        TextBlock welcomeMsg;
        TextBlock nonselect;
        TextBlock scoreBoard;
        TextBlock waveblk;

        ComboBox diffculty;

        Image charmander, pikatchu;
        Image img1;
        private int elapsedSec;
        Sounds Sounds = new Sounds();

        public Board(Canvas MainCanvas, Image image, Button exit, Canvas canvas, Button exit1)
        {
            songPlayer = new MediaPlayer();
            Sounds Sounds = new Sounds();

            elapsedSec = 0;
            var timer2 = new System.Timers.Timer(1000);
            timer2.Elapsed += (Object source, ElapsedEventArgs e) =>
            {
                elapsedSec++;
            };
            timer2.AutoReset = true;
            timer2.Enabled = true;

            exit2 = exit1;
            secondboard = canvas;
            gameOver = exit;
            img1 = image;
            boardCanvas = MainCanvas;
  
            UserInterface();

            boardCanvas.Visibility = Visibility.Collapsed;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Timer_Tick;
        }

        //User interface 
        public void UserInterface()
        {
            // start Button
            startbtn = UIElementsFactory.CreateStartBtn();
            startbtn.Click += Startbtn_Click;
            startbtn.PointerEntered += Startbtn_PointerEntered;
            startbtn.PointerExited += Startbtn_PointerExited;
            secondboard.Children.Add(startbtn);
            Canvas.SetTop(startbtn, 470);
            Canvas.SetLeft(startbtn, 780);

            // Tryagain Button
            tryAgain = UIElementsFactory.CreatTryAgainBtn();
            tryAgain.Click += TryAgain_Click; ; ;
            tryAgain.PointerMoved += TryAgain_PointerMoved;
            tryAgain.PointerExited += TryAgain_PointerExited;
            boardCanvas.Children.Add(tryAgain);
            Canvas.SetTop(tryAgain, 540);
            Canvas.SetLeft(tryAgain, 870);
            Canvas.SetZIndex(tryAgain, 4);

            // Time survived txt block
            surv = UIElementsFactory.TimeSurvivedTXTblock();
            Canvas.SetTop(surv, 5);
            Canvas.SetLeft(surv, 20);
            Canvas.SetZIndex(surv, 4);
            boardCanvas.Children.Add(surv);

            //score board txt block
            score = UIElementsFactory.ScoreBoardTimeTXTblock();
            Canvas.SetTop(score, 320);
            Canvas.SetLeft(score, 680);
            boardCanvas.Children.Add(score);

            //welcome messege
            welcomeMsg = UIElementsFactory.WelcomeMsg();
            Canvas.SetTop(welcomeMsg, 20);
            Canvas.SetLeft(welcomeMsg, 350);
            secondboard.Children.Add(welcomeMsg);

            //diffcult level
            diffculty = UIElementsFactory.SelectDiffcultyCB();
            Canvas.SetTop(diffculty, 600);
            Canvas.SetLeft(diffculty, 820);
            secondboard.Children.Add(diffculty);
            diffculty.SelectionChanged += Diffculty_SelectionChanged;

            //adding char+pika to menu
            charmander = UIElementsFactory.AddCharmander();
            Canvas.SetTop(charmander, 200);
            Canvas.SetLeft(charmander, 1400);
            secondboard.Children.Add(charmander);

            pikatchu = UIElementsFactory.AddPikactchu();
            Canvas.SetTop(pikatchu, 200);
            Canvas.SetLeft(pikatchu, 100);
            secondboard.Children.Add(pikatchu);

            //adding error block to combo
            nonselect = UIElementsFactory.ErorrNonSelectedBLK();
            Canvas.SetTop(nonselect, 550);
            Canvas.SetLeft(nonselect, 770);
            secondboard.Children.Add(nonselect);

            //adding menu button
            Menubtn = UIElementsFactory.CreateMenuBTN();
            Canvas.SetTop(Menubtn, 650);
            Canvas.SetLeft(Menubtn, 870);
            Canvas.SetZIndex(Menubtn, 2);
            boardCanvas.Children.Add(Menubtn);
            Menubtn.Click += Menubtn_Click;
            Menubtn.PointerMoved += Menubtn_PointerMoved;
            Menubtn.PointerExited += Menubtn_PointerExited;

            //adding score board
            scoreBoard = UIElementsFactory.CreateScoreBoard();
            Canvas.SetTop(scoreBoard, 8);
            Canvas.SetLeft(scoreBoard, 600);
            boardCanvas.Children.Add(scoreBoard);
            scoreBoard.Visibility = Visibility.Visible;

            //adding wave block
            waveblk = UIElementsFactory.CreateScoreBoard();
            Canvas.SetTop(waveblk, 8);
            Canvas.SetLeft(waveblk, 950);
            boardCanvas.Children.Add(waveblk);
            waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            waveblk.Text = $"Wave: {waveNum++}      Enemies: {size}";
            waveblk.Visibility = Visibility.Visible;
            waveblk.Width = 700;

        }
        public void WaveColorChange()
        {
           
            if (waveNum <= 5) waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            if (waveNum > 5 && waveNum <= 10) waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Aqua);
            if (waveNum > 10 && waveNum <= 15) waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Yellow);
            if (waveNum > 15 && waveNum <= 20) waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Orange);
            if (waveNum > 20 && waveNum <= 25) waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            if (waveNum == 6)  Sounds.WaveUpSoundAsync();
            if (waveNum == 11) Sounds.WaveUpSoundAsync(); 
            if (waveNum == 16) Sounds.WaveUpSoundAsync(); 
            if (waveNum == 21) Sounds.WaveUpSoundAsync(); 
           
        }

        //click events
        private void Menubtn_Click(object sender, RoutedEventArgs e)
        {
            boardCanvas.Visibility = Visibility.Collapsed;
            secondboard.Visibility = Visibility.Visible;
            Menubtn.Visibility = Visibility.Collapsed;
            tryAgain.Visibility = Visibility.Collapsed;
            gameOver.Visibility = Visibility.Collapsed;
            exit2.Visibility = Visibility.Collapsed;
            img1.Visibility = Visibility.Collapsed;
            score.Visibility = Visibility.Collapsed;
            surv.Visibility = Visibility.Visible;
            scoreBoard.Text = "Score:0 ";
            scoreBoard.FontSize = 50;
            Canvas.SetTop(scoreBoard, 8);
            waveblk.Text = $"Wave: {waveNum = 1}      Enemies: {size}";
            waveblk.FontSize = 50;
            Canvas.SetTop(waveblk, 8);
            elapsedSec = 0;
            CreateEnemies(size);
            CreatePlayer();
            CreatePokeBall();
            ClearBoard();

        }
        private void TryAgain_Click(object sender, RoutedEventArgs e)
        {
            tryAgain.Visibility = Visibility.Collapsed;
            gameOver.Visibility = Visibility.Collapsed;
            exit2.Visibility = Visibility.Collapsed;
            img1.Visibility = Visibility.Collapsed;
            score.Visibility = Visibility.Collapsed;
            surv.Visibility = Visibility.Visible;
            Menubtn.Visibility = Visibility.Collapsed;
            secondboard.Visibility = Visibility.Collapsed;
            scoreBoard.Visibility = Visibility.Visible;
            waveblk.Visibility = Visibility.Visible;
            scoreBoard.Text = "Score:0 ";
            scoreBoard.FontSize = 50;
            Canvas.SetTop(scoreBoard, 8);
            waveblk.Text = $"Wave: {waveNum = 1}      Enemies: {size}";
            waveblk.FontSize = 50;
            Canvas.SetTop(waveblk, 8);

            elapsedSec = 0;
            scoreValue = 0;
            CreatePlayer();
            CreatePokeBall();
            CreateEnemies(size);


            timer.Start();
        }
        public void Startbtn_Click(object sender, RoutedEventArgs e)
        {
            if (diffculty.SelectedItem == null)
            {
                nonselect.Text = $"Please selcet difficulty!";
            }
            else
            {
                timer.Start();


                boardCanvas.Visibility = Visibility.Visible;
                tryAgain.Visibility = Visibility.Collapsed;
                gameOver.Visibility = Visibility.Collapsed;
                exit2.Visibility = Visibility.Collapsed;
                img1.Visibility = Visibility.Collapsed;
                score.Visibility = Visibility.Collapsed;
                surv.Visibility = Visibility.Visible;
                Menubtn.Visibility = Visibility.Collapsed;
                secondboard.Visibility = Visibility.Collapsed;
                scoreBoard.Visibility = Visibility.Visible;
                scoreBoard.Text = "Score:0 ";
                scoreBoard.FontSize = 50;
                Canvas.SetTop(scoreBoard, 8);
                waveblk.Text = $"Wave: {waveNum = 1}      Enemies: {size}";
                waveblk.FontSize = 50;
                Canvas.SetTop(waveblk, 8);

                elapsedSec = 0;
                scoreValue = 0;
                CreateEnemies(size);
                CreatePlayer();
                CreatePokeBall();

            }
        }

        //diffculty level
        private void Diffculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (diffculty.SelectedItem == "Expert")
            {
                playerStep = 15;
                size = 4;

            }
            if (diffculty.SelectedItem == "Hard")
            {
                playerStep = 15;
                size = 3;

            }
            if (diffculty.SelectedItem == "Medium")
            {
                playerStep = 15;
                size = 2;

            }
            if (diffculty.SelectedItem == "Easy")
            {
                playerStep = 15;
                size = 1;

            }
            elapsedSec = 0;
        }

        //Collisions funcs
        public void CatchPokeBall()
        {
            for (int i = 0; i < poke.Count; i++)
            {
                if (Math.Abs(poke[i].X - player.X) < (player.Width / 2) - 10 && Math.Abs(poke[i].Y - player.Y) < (player.Height / 2) - 10)
                {

                    boardCanvas.Children.Remove(poke[i].Element);
                    poke.Remove(poke[i]);
                    i--;
                    
                    CreatePokeBall();
                    scoreValue += 10;
                    scoreBoard.Text = "Score: " + scoreValue;
                    Sounds.CatchPokeSoundAsync();
                    if (scoreValue == limit)
                    {
                        if (diffculty.SelectedItem == "Easy") { size++; }
                        if (diffculty.SelectedItem == "Medium") { size += 2; }
                        if (diffculty.SelectedItem == "Hard") { size += 3; }
                        if (diffculty.SelectedItem == "Expert") { size += 4; }

                        waveNum++;
                        waveblk.Text = $"Wave: {waveNum}      Enemies: {size}";
                        limit += 40;
                        WaveColorChange();
                        ClearBoard();
                        CreateEnemies(size);
                        CreatePlayer();
                        CreatePokeBall();

                    }
                }
            }
        }
        private void PlayerCatched(Enemy enemy)
        {
            if (Math.Abs(enemy.X - player.X) < 10 && Math.Abs(enemy.Y - player.Y) < 10)
            {
                timer.Stop();
                ClearBoard();
                img1.Visibility = Visibility.Visible;
                gameOver.Visibility = Visibility.Visible;
                tryAgain.Visibility = Visibility.Visible;
                score.Visibility = Visibility.Visible;
                Menubtn.Visibility = Visibility.Visible;
                Canvas.SetTop(scoreBoard, 200);
                scoreBoard.FontSize = 60;
                Canvas.SetTop(waveblk, 200);
                waveblk.FontSize = 60;
                score.Text = "You Survived " + elapsedSec.ToString() + " Seconds";

                if (diffculty.SelectedItem == "Expert") size = 4;
                if (diffculty.SelectedItem == "Hard") size = 3;
                if (diffculty.SelectedItem == "Medium") size = 2;
                if (diffculty.SelectedItem == "Easy") size = 1;

            }
        }

        // create funnctions
        public void CreatePokeBall()
        {
            Random random = new Random();
            poke = new List<Pokeballs>();
            for (int i = 0; i < 1; i++)
            {
                Pokeballs pokeballs = new Pokeballs();
                pokeballs.Y = random.Next(30, 930);
                pokeballs.X = random.Next(10, 1800);
                pokeballs.Height = 70;
                pokeballs.Width = 70;
                boardCanvas.Children.Add(pokeballs.Element);
                poke.Add(pokeballs);
            }



        }
        private void CreatePlayer()
        {
            player = new Player();
            boardCanvas.Children.Add(player.Element);
            player.Z = 4;
            player.X = 850;
            player.Y = 0;
        }
        private void CreateEnemies(int amount)
        {
            Random random = new Random();
            enemies = new List<Enemy>();

            for (int i = 0; i < amount; i++)
            {
                Enemy e = new Enemy();
                e.X = random.Next(0, 2400);
                e.Y = random.Next(600, 1550);
                boardCanvas.Children.Add(e.Element);
                enemies.Add(e);
            }
        }

        // pointers events
        private void Startbtn_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            startbtn.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
        }
        private void Startbtn_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            startbtn.Background = new SolidColorBrush(Windows.UI.Colors.IndianRed);
        }
        private void TryAgain_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            tryAgain.Background = new SolidColorBrush(Windows.UI.Colors.Red);
        }
        private void TryAgain_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            tryAgain.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }
        private void Menubtn_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Menubtn.Background = new SolidColorBrush(Windows.UI.Colors.Red);
        }
        private void Menubtn_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Menubtn.Background = new SolidColorBrush(Windows.UI.Colors.White);
        }

        // clear board function
        public void ClearBoard()
        {
            foreach (var ene in enemies)
            {
                boardCanvas.Children.Remove(ene.Element);
            }
            foreach (var pok in poke)
            {
                boardCanvas.Children.Remove(pok.Element);
            }
            boardCanvas.Children.Remove(player.Element);
        }

        // Movements & ticks
        public void Timer_Tick(object sender, object e)
        {
            foreach (var enemy in enemies)
            {
                enemy.Move(player);
                PlayerCatched(enemy);
            }
            CatchPokeBall();


            DoMovement();
            if (moveLeft)
            {
                player.PrintIMG("left");
            }
            if (moveRight)
            {
                player.PrintIMG("right");
            }

            surv.Text = "Time Survived: " + Convert.ToString(elapsedSec) + " Sec";
        }
        public void DoMovement()
        {
            if (moveLeft)
            { if (player.X >= 20) player.X -= playerStep; }
            if (moveRight)
            { if (player.X + player.Width <= boardCanvas.ActualWidth) player.X += playerStep; }
            if (moveUp)
            { if (player.Y >= 20) player.Y -= playerStep; }
            if (moveDown)
            { if (player.Y + player.Height <= boardCanvas.ActualHeight) player.Y += playerStep; }
        }
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.VirtualKey == VirtualKey.Up) moveUp = true;
            if (e.VirtualKey == VirtualKey.Down) moveDown = true;
            if (e.VirtualKey == VirtualKey.Left) moveLeft = true;
            if (e.VirtualKey == VirtualKey.Right) moveRight = true;
            DoMovement();
        }
        public void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.VirtualKey == VirtualKey.Up) moveUp = false;
            if (e.VirtualKey == VirtualKey.Down) moveDown = false;
            if (e.VirtualKey == VirtualKey.Left) moveLeft = false;
            if (e.VirtualKey == VirtualKey.Right) moveRight = false;
        }

        // sounds affects

       
    }
}
