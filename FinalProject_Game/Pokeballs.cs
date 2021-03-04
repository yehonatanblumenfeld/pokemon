

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
