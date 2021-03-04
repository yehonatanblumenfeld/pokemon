

namespace FinalProject_Game
{
    public class Player : GamePiece
    {   
        public int ImgIndex = 0;
        public int Side = 1;

        #region player pictures & direction
        public override void PrintIMG(string direction1)
        {
            if (direction1 == "")
            {
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaright1.gif";
                    ImgIndex++;
                    ImgIndex = 0;
                    base.PrintIMG(direction1);
                }
            }
            if (direction1 == "right")
            {
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaright{ImgIndex}.gif";
                    ImgIndex++;
                    Side = 1;
                    if (ImgIndex > 3) ImgIndex = 0;
                    base.PrintIMG(direction1);
                }
            }
            if (direction1 == "left")
            {
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaleft{ImgIndex}.gif";
                    ImgIndex++;
                    Side = 0;
                    if (ImgIndex > 3) ImgIndex = 0;
                    base.PrintIMG(direction1);
                }
            }
            if (direction1 == "down")
            {
                if (Side == 1)
                {                   
                    imageUri = $@"ms-appx:///Assets/pika/pikaright{ImgIndex}.gif";
                    ImgIndex++;
                    if (ImgIndex > 3) ImgIndex = 0;
                    base.PrintIMG(direction1);
                }
                if (Side == 0)
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaleft{ImgIndex}.gif";
                    ImgIndex++;
                    if (ImgIndex > 3) ImgIndex = 0;
                    base.PrintIMG(direction1);
                }
            }
            if (direction1 == "up")
            {
                if (Side == 1)
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaright{ImgIndex}.gif";
                    ImgIndex++;
                    if (ImgIndex > 3) ImgIndex = 0;
                    base.PrintIMG(direction1);
                }
                if (Side == 0)
                {
                    imageUri = $@"ms-appx:///Assets/pika/pikaleft{ImgIndex}.gif";
                    ImgIndex++;
                    if (ImgIndex > 3) ImgIndex = 0;
                    base.PrintIMG(direction1);
                }
                #endregion
            }
        }      
    }    
}

