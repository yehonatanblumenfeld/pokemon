using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FinalProject_Game
{
    class Sounds
    {

        MediaPlayer songPlayer;
        bool playing;
        
        // wave up sound
        public async Task WaveUpSoundAsync()
        {
            songPlayer = new MediaPlayer();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("pokeMusic.mp3");
            songPlayer.AutoPlay = false;
            songPlayer.Source = MediaSource.CreateFromStorageFile(file);
            songPlayer.Volume = 0.05;

            if (playing)
            {
                songPlayer.Source = null;
                playing = false;
            }
            else
            {
                songPlayer.Play();
                playing = true;
            }

        }
        public async Task CatchPokeSoundAsync()
        {
            songPlayer = new MediaPlayer();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets/sounds");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("pokeballsound.mp3");
            songPlayer.AutoPlay = false;
            songPlayer.Source = MediaSource.CreateFromStorageFile(file);
            songPlayer.Volume = 0.05;

            if (playing)
            {
                songPlayer.Source = null;
                playing = false;
            }
            else
            {
                songPlayer.Play();
                playing = true;
            }
        }
        }
}
