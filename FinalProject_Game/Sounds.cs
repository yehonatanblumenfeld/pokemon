using System;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FinalProject_Game
{
     public class Sounds
    {
         public MediaPlayer SongPlayer;

        #region wave up sound
        public async Task WaveUpSoundAsync()
        {
            SongPlayer = new MediaPlayer();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("waveuppika.mp3");
            SongPlayer.AutoPlay = false;
            SongPlayer.Source = MediaSource.CreateFromStorageFile(file);
            SongPlayer.Volume = 0.15;
            SongPlayer.Play();
        }
        public async Task CatchPokeSoundAsync()
        {
            SongPlayer = new MediaPlayer();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("pokeballcatchsound.mp3");
            SongPlayer.AutoPlay = false;
            SongPlayer.Source = MediaSource.CreateFromStorageFile(file);
            SongPlayer.Volume = 0.15;
            SongPlayer.Play();
        }
        #endregion
    }

}
