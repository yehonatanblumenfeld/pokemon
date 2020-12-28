using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Game
{
    public class Enemy : GamePiece
    {
        private double step;
        public override void PrintIMG(string direction)
        {
            imageUri = @"ms-appx:///Assets/dragon gif.gif";           
            step = new Random().Next(4,10);
            base.PrintIMG(direction);
        }
        public void Move(GamePiece player)
        {
            if (this.X > player.X)
            {
                this.X -= step;
            }
            if (this.X < player.X)
            {
                this.X += step;
            }
            if (this.Y > player.Y)
            {
                this.Y -= step;
            }
            if (this.Y < player.Y)
            {
                this.Y += step;
            }
         
            }
    }
}
