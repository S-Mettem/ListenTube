using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ListenTube
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        MainWindow MAIN_WINDOW;

        public PlayerWindow(MainWindow mainwindow, String mp3name)
        {
            InitializeComponent();
            this.MAIN_WINDOW = mainwindow;
            this.MediaPlayer.Source = new Uri("ms-appx:///" + mp3name);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MediaPlayer.Play();
        }
    }
}
