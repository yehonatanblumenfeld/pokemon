using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Media.Playback;
using Windows.Media.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalProject_Game
{   
    public partial class MainPage : Page
    {
        MediaPlayer songPlayer;
        bool playing;        
        GameLogic logic;
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            
            songPlayer = new MediaPlayer();
        }
        // background music btn--------------------------------------------------
        private async void musicbtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("pokeMusic.mp3");           
            songPlayer.AutoPlay = false;
            songPlayer.Source = MediaSource.CreateFromStorageFile(file);
            songPlayer.Volume = 0.02;

            if (playing)
            {                
                songPlayer.Source = null;
                playing = false;
                musicbtn.Content = "Play Music";
            }
            else
            {                           
                songPlayer.Play();
                playing = true;
                musicbtn.Content = "Pause Music";           
            }
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic(MainCanvas ,img ,btnexit1 , MenuCanvas  , btnexit1 , cnvsControls);          
        }
        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {           
            logic.KeyDown(sender , args);           
        }
        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            logic.KeyUp(sender, args);
        }
        
        // exit  btn--------------------------------------------------
        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();          
        }

        
    }
}
