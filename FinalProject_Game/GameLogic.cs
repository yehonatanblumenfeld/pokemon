using System;
using System.Threading;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FinalProject_Game
{
    public class GameLogic
    {
        #region logic properties
        public Board board;
        bool moveUp, moveDown, moveLeft, moveRight;
        #endregion
        public GameLogic(Canvas canvas, Image img, Button exit, Canvas secondCanvas, Button exit1 , Canvas controls)
        {
            board = new Board(canvas, img, exit, secondCanvas, exit1 , controls);
            board.Timer.Interval = TimeSpan.FromMilliseconds(20);
            board.Timer.Tick += Timer_Tick;
        }
        #region Movements & ticks
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.VirtualKey == VirtualKey.Up    || e.VirtualKey == VirtualKey.W) moveUp = true;
            if (e.VirtualKey == VirtualKey.Down  || e.VirtualKey == VirtualKey.S) moveDown = true;
            if (e.VirtualKey == VirtualKey.Left  || e.VirtualKey == VirtualKey.A) moveLeft = true;
            if (e.VirtualKey == VirtualKey.Right || e.VirtualKey == VirtualKey.D) moveRight = true;
            
            DoMovement();
        }
        public void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.VirtualKey == VirtualKey.Up   || e.VirtualKey == VirtualKey.W) moveUp = false;
            if (e.VirtualKey == VirtualKey.Down || e.VirtualKey == VirtualKey.S) moveDown = false;
            if (e.VirtualKey == VirtualKey.Left || e.VirtualKey == VirtualKey.A) moveLeft = false;
            if (e.VirtualKey == VirtualKey.Right|| e.VirtualKey == VirtualKey.D) moveRight = false;
            
        }
        public void DoMovement()
        {
            if (moveLeft)
            { if (board.Player.X >= 20) board.Player.X -= board.PlayerStep; }
            if (moveRight)
            { if (board.Player.X + board.Player.Width <= board.BoardCanvas.ActualWidth) board.Player.X += board.PlayerStep; }
            if (moveUp)
            { if (board.Player.Y >= 20) board.Player.Y -= board.PlayerStep; }
            if (moveDown)
            { if (board.Player.Y + board.Player.Height <= board.BoardCanvas.ActualHeight) board.Player.Y += board.PlayerStep; }

        }
        public void Timer_Tick(object sender, object e)
        {
            foreach (var enemy in board.Enemies)
            {
                enemy.Move(board.Player);
                PlayerCatched(enemy);
            }
            CatchPokeBallAsync();
            DoMovement();
            DirectionString();

            board.TimeSurvivedBlk.Text = "Time Survived: " + Convert.ToString(board.ElapsedSec) + " Sec";
        }
        public void DirectionString()
        {
            if (moveLeft)
            {
                board.Player.PrintIMG("left");
            }
            else if (moveRight)
            {
                board.Player.PrintIMG("right");
            }
            else if (moveUp)
            {
                board.Player.PrintIMG("up");
            }
            else if (moveDown)
            {
                board.Player.PrintIMG("down");
            }
        }
        #endregion

        #region Collisions funcs
        public void PlayerCatched(Enemy enemy)
        {
            if (Math.Abs(enemy.X - board.Player.X) < 10 && Math.Abs(enemy.Y - board.Player.Y) < 10)
            {
                board.Timer.Stop();
                board.GameOverImg.Visibility = Visibility.Visible;
                board.Exitbtn.Visibility = Visibility.Visible;
                board.TryAgain.Visibility = Visibility.Visible;
                board.Score.Visibility = Visibility.Visible;
                board.Menubtn.Visibility = Visibility.Visible;
                Canvas.SetTop(board.ScoreBoard, 200);
                board.ScoreBoard.FontSize = 60;
                Canvas.SetTop(board.Waveblk, 200);
                board.Waveblk.FontSize = 60;
                board.Score.Text = "You Survived " + board.ElapsedSec.ToString() + " Seconds";
                board.Waveblk.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);

                if (board.Diffculty.SelectedItem.ToString() == "Expert") board.Size = 4;
                if (board.Diffculty.SelectedItem.ToString() == "Hard") board.Size = 3;
                if (board.Diffculty.SelectedItem.ToString() == "Medium") board.Size = 2;
                if (board.Diffculty.SelectedItem.ToString() == "Easy") board.Size = 1;
                board.ClearBoard();

            }
        }
        public async void CatchPokeBallAsync()
        {
            for (int i = 0; i < board.Poke.Count; i++)
            {
                if (Math.Abs(board.Poke[i].X - board.Player.X) < (board.Player.Width / 2) - 10 && Math.Abs(board.Poke[i].Y - board.Player.Y) < (board.Player.Height / 2) - 10)
                {
                    await board.Sounds.CatchPokeSoundAsync();
                    board.BoardCanvas.Children.Remove(board.Poke[i].Element);
                    board.Poke.Remove(board.Poke[i]);
                    board.ScoreValue += 10;
                    i--;

                    board.CreatePokeBall();

                    board.ScoreBoard.Text = "Score: " + board.ScoreValue;

                    if (board.ScoreValue == board.Limit)
                    {
                        if (board.Diffculty.SelectedItem.ToString() == "Easy")   { board.Size++; }
                        if (board.Diffculty.SelectedItem.ToString() == "Medium") { board.Size += 2; }
                        if (board.Diffculty.SelectedItem.ToString() == "Hard")   { board.Size += 3; }
                        if (board.Diffculty.SelectedItem.ToString() == "Expert") { board.Size += 4; }
                        await board.Sounds.WaveUpSoundAsync();
                        board.Limit += 40;
                        board.WaveNum++;
                        board.Waveblk.Text = $"Wave: {board.WaveNum}      Enemies: {board.Size}";
                        board.WaveColorChange();
                        board.ClearBoard();
                        board.CreateEnemies(board.Size);
                        board.CreatePlayer();
                        board.CreatePokeBall();
                        Thread.Sleep(200);
                    }
                }
            }
        }
        #endregion
    }

}
