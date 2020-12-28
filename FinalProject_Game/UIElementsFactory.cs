using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject_Game
{
    class UIElementsFactory
    {


        public static Button CreateStartBtn()
        {
            Button startbtn = new Button();
            startbtn.Content = "New Game";
            startbtn.Width = 350;
            startbtn.Height = 80;
            startbtn.FontSize = 60;
            startbtn.FontStyle = Windows.UI.Text.FontStyle.Italic;
            //var brush = new ImageBrush();
            //brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/buttonbackground.jpg"));
            //brush.Stretch = Stretch.UniformToFill;
            //startbtn.Background = brush;
            startbtn.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
            
            startbtn.BorderThickness = new Thickness(3, 3, 3, 3);
           
            return startbtn;
        }
        public static Button CreatTryAgainBtn()
        {
            Button tryAgain = new Button();
            tryAgain.Content = "Try Again";
            tryAgain.Width = 200;
            tryAgain.Height = 60;
            tryAgain.FontSize = 35;
            tryAgain.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
            tryAgain.BorderThickness = new Thickness(3, 3, 3, 3);
            tryAgain.Visibility = Visibility.Collapsed;
            tryAgain.FontStyle = Windows.UI.Text.FontStyle.Italic;
            tryAgain.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            return tryAgain;
        }
        public static TextBlock TimeSurvivedTXTblock()
        {
            TextBlock surv = new TextBlock();
            surv.Text = "0";
            surv.Width = 300;
            surv.Height = 60;
            surv.FontSize = 25;
            surv.Foreground = new SolidColorBrush(Windows.UI.Colors.Crimson);
            return surv;
        }
        public static TextBlock ScoreBoardTimeTXTblock()
        {
            TextBlock score = new TextBlock();
            score.Text = "0";
            score.Width = 650;
            score.Height = 60;
            score.FontSize = 50;
            score.Visibility = Visibility.Collapsed;
            Windows.UI.Xaml.Documents.Typography.SetCapitals(score, FontCapitals.SmallCaps);
            score.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            return score;
        }
        public static TextBlock WelcomeMsg()
        {
            TextBlock welcomemsg = new TextBlock();
            welcomemsg.FontSize = 100;
            welcomemsg.Width = 1500;
            welcomemsg.Height = 150;
            welcomemsg.Text = "Welcome to the Dodge Game!";
            Windows.UI.Xaml.Documents.Typography.SetCapitals(welcomemsg, FontCapitals.SmallCaps);
            welcomemsg.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkRed);
            return welcomemsg;
        }
        public static ComboBox SelectDiffcultyCB()
        {
            ComboBox diffcult = new ComboBox();
            diffcult.PlaceholderText = " Select Diffculty";
            diffcult.Items.Add("Expert");
            diffcult.Items.Add("Hard");
            diffcult.Items.Add("Medium");
            diffcult.Items.Add("Easy");
            diffcult.Width = 250;
            diffcult.Height = 50;
            diffcult.FontSize = 30;
            diffcult.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            return diffcult;
        }
        public static Image AddCharmander()
        {
            Image charmander = new Image();
            charmander.Source = new BitmapImage(new Uri("ms-appx:///Assets/charmander.png"));
            charmander.Height = 500;
            charmander.Width = 500;
            return charmander;
        }
        public static Image AddPikactchu()
        {
            Image pickatchu = new Image();
            pickatchu.Source = new BitmapImage(new Uri("ms-appx:///Assets/pika.png"));
            pickatchu.Height = 500;
            pickatchu.Width = 500;
            return pickatchu;
        }
        public static TextBlock ErorrNonSelectedBLK()
        {
            TextBlock nonselect = new TextBlock();
            nonselect.FontSize = 35;
            nonselect.Width = 380;
            nonselect.Height = 50;
            nonselect.Text = "";
            Windows.UI.Xaml.Documents.Typography.SetCapitals(nonselect, FontCapitals.SmallCaps);
            nonselect.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);

            return nonselect;
        }
        public static Button CreateMenuBTN()
        {
            Button back = new Button();
            back.Content = "Menu";
            back.Height = 60;
            back.Width = 200;
            back.BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
            back.BorderThickness = new Thickness(3,3,3,3);
            back.FontSize = 35;
            back.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            back.FontStyle = Windows.UI.Text.FontStyle.Italic;
            back.Visibility = Visibility.Collapsed;
            return back;
        }
        public static TextBlock CreateScoreBoard()
        {
            TextBlock scoreBoard = new TextBlock();
            scoreBoard.Text = "score: 0";
            scoreBoard.Width = 350;
            scoreBoard.Height = 70;
            scoreBoard.FontSize = 50;
            scoreBoard.Foreground = new SolidColorBrush(Windows.UI.Colors.Yellow);
            scoreBoard.FontStyle = Windows.UI.Text.FontStyle.Oblique;
            scoreBoard.Visibility = Visibility.Collapsed;
            return scoreBoard;
        }
    }
}
