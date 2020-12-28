using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Game
{
    public class Player : GamePiece
    { public int i = 1;
        public override void PrintIMG(string direction1)
        {


            if (direction1 == "right")
            {
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaright{i}.gif";
                    i++;
                    if (i > 4) i = 1;
                    base.PrintIMG(direction1);
                }
            }
            if (direction1 == "left")
            {
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaleft{i}.gif";
                    i++;
                    if (i > 4) i = 1;
                    base.PrintIMG(direction1);
                }
            }
        }      
    }
    
}

