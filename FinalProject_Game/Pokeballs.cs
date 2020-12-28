using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FinalProject_Game
{
     public class Pokeballs : GamePiece
    {
        public override void PrintIMG(string direction)
        {
            imageUri = @"ms-appx:///Assets/pokemon.gif";
            base.PrintIMG(direction);
     
        }
      
    }
}
