using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace FinalProject_Game
{
       public class GameLogic


              
    {
        public Board board;
        

        public GameLogic(Canvas canvas , Image img , Button exit , Canvas secondCanvas , Button exit1 )
        {
            
            board = new Board(canvas , img , exit , secondCanvas , exit1);
            
        }


    }
}
