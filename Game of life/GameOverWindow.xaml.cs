using System;
using System.Windows;
using WpfAnimatedGif; 
using System.Windows.Media.Imaging; // Für BitmapImage

namespace Game_of_life
{
    public partial class GameOverWindow : Window
    {
     
        public GameOverWindow()
        {

           
            InitializeComponent();

           
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("C:\\Users\\buerklele\\source\\repos\\Game of life\\Game of life\\Resources\\you-died-cat.gif"); // Pfad zur GIF-Datei
            image.EndInit();

            
            ImageBehavior.SetAnimatedSource(gifImage, image);

            this.Topmost = true; //Vordergrund?

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;   // Stellt sicher, dass das Fenster immer im Vordergrund bleibt
            this.Activate();       // Setzt den Fokus auf das Fenster
        }
    }
}
