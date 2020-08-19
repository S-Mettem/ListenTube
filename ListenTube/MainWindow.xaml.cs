using System;
using System.Text.RegularExpressions;
using System.Windows;


namespace ListenTube
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String textFromUrl = this.UrlText.Text;

            if (IsUrlValid(textFromUrl))
            {
                String[] isYoutube;
                isYoutube = textFromUrl.Split('.');

                if ((isYoutube[1]) == "youtube")
                {
                    this.Visibility = Visibility.Hidden;
                    WaitingWindow wait = new WaitingWindow(this, textFromUrl);
                    wait.Show();
                }
                else
                {
                    MessageBox.Show("It is not a Youtube link!");
                }
            }
            else
            {
                MessageBox.Show("Url is not valid");
            }
        }

        private bool IsUrlValid(string url)
        {
            return Regex.IsMatch(url, @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
    }
}
