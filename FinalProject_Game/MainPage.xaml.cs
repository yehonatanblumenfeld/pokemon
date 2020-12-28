using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalProject_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
        private async void musicbtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("pokeMusic.mp3");           
            songPlayer.AutoPlay = false;
            songPlayer.Source = MediaSource.CreateFromStorageFile(file);
            songPlayer.Volume = 0.05;

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
            logic = new GameLogic(MainCanvas ,img ,btnexit1 , MenuCanvas  , btnexit1);
            
        }
        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            
            logic.board.KeyDown(sender , args);           
        }
        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            logic.board.KeyUp(sender, args);
        }      
        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
           
        }

        
    }
}
