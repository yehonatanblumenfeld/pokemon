using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject_Game
{
    public class GamePiece
    {
        protected string imageUri = "";

        public Image Element { get; set; }
        public double X
        {
            get
            {
                return Canvas.GetLeft(Element );
            }
            set
            {
                Canvas.SetLeft(Element, value);
            }
        }
        public double Y
        {
            get
            {
                return Canvas.GetTop(Element);
            }
            set
            {
                
                Canvas.SetTop(Element, value);
            }
        }
        public int Z
        {
            get
            {
                return Canvas.GetZIndex(Element);
            }
            set
            {

                Canvas.SetZIndex(Element, value);
            }
        }
        public double Height
        {
            get
            {
                return Element.ActualHeight;
            }
            set
            {
                Element.Height = value;
            }
        }
        public double Width
        {
            get
            {
                return Element.ActualWidth;
            }
            set
            {
                Element.Width = value;
            }
        }
        public GamePiece()
        {
            Element = new Image();
            Element.Stretch = Stretch.Uniform;
            Height = 150;
            Width = 150;           
            PrintIMG("");
        }
        public virtual void PrintIMG(string direction)
        {
            Element.Source = new BitmapImage(new Uri(imageUri));
          
        }
    }
}
