using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using VideoLibrary;

namespace ListenTube
{
    public partial class WaitingWindow : Window
    {
        private String URL; // URL from previos Window
        MainWindow MAIN_WINDOW; // Previos Window
        String MP3NAME; // Name of loaded file

        public WaitingWindow(MainWindow mainwindow ,String urlFromTextBox)
        {
            InitializeComponent();
            this.URL = urlFromTextBox;
            this.MAIN_WINDOW = mainwindow;

            // That code needed for progress bar and async thread
            pbCalculationProgress.Value = 0; // Set progress bar to 0
            BackgroundWorker worker = new BackgroundWorker(); // init BackgroundWorkder for async thread
            worker.WorkerReportsProgress = true; // To bind to progress bar
            worker.DoWork += worker_DoWork; // Start downloads video from youtube and convert to MP3 file
            worker.ProgressChanged += worker_ProgressChanged; // Needed to change progress bar
            worker.RunWorkerCompleted += worker_RunWorkerCompleted; 
            worker.RunWorkerAsync(100);
        }

        
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            String source = "tmp/";
            (sender as BackgroundWorker).ReportProgress(20, 3);

            // Download video from URL
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(this.URL);
            File.WriteAllBytes(source + video.FullName, video.GetBytes());
            (sender as BackgroundWorker).ReportProgress(40, 3);

            // Convert video to audio
            this.MP3NAME = "audio/" + video.FullName;
            var inputFile = new MediaFile { Filename = $"{source + video.FullName}" };
            var outputFile = new MediaFile { Filename = $"{this.MP3NAME}" + ".mp3" };

            (sender as BackgroundWorker).ReportProgress(60, 3);

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                (sender as BackgroundWorker).ReportProgress(80, 3);

                engine.Convert(inputFile, outputFile);
            }
            (sender as BackgroundWorker).ReportProgress(100, 3);
            File.Delete(source + video.FullName);

        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbCalculationProgress.Value = e.ProgressPercentage;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.MAIN_WINDOW.Visibility = Visibility.Visible;

        }
    }
}
