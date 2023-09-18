using MusicPlayer.Naudio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string url = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            new TaskFactory().StartNew(() =>
            {
                StartSearchingSongs();
            });
        }

        private bool IsPingSuccess()
        {
            var strResult = HTTPHelper.GetXML("ping").Result;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(strResult);
            var statusElem = xml.GetElementsByTagName("subsonic-response").Item(0);
            var sttr = statusElem?.Attributes?.GetNamedItem("status")?.Value ?? "";

            return sttr.Contains("ok");
        }

        private void StartSearchingSongs()
        {
            if (IsPingSuccess())
            {
                var songsSTR = HTTPHelper.GetXML("search3", "query=\"\"").Result;
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(songsSTR);
                var allSongsElem = xml.GetElementsByTagName("song");
                var firstSong = allSongsElem.Item(0);
                var songid = firstSong.Attributes.GetNamedItem("id").Value;

                var fullURL = HTTPHelper.BuildURL("stream", "id=" + songid);
                url = fullURL;

            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void PackIconMaterial_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(url))
            {
                mp3pannel mp3player = new mp3pannel();
                mp3player.play(url);
            }
        }
    }
}
