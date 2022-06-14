using System;
using System.Collections.Generic;
using System.Windows;

namespace SubReNamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGridNamesDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                List<string> fileList = new((string[])e.Data.GetData(DataFormats.FileDrop));
                fileList.Sort(StringComparer.Ordinal);
                Utils.GetVideoAndSubList(fileList, out List<string> videoList, out List<string> subList);

                this.AddVideoList(videoList);
                this.AddSubtitleList(subList);

                this.DataGridNames.Items.Refresh();
            }
        }

        private void PreviewClick(object sender, RoutedEventArgs e)
        {
            this.SetNewSubNames();
        }


        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            this.SetNewSubNames(applyRename: true);
            MessageBox.Show("Succeeded!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SetNewSubNames(bool applyRename = false)
        {
            string suffix = this.suffixTextBox.Text;
            foreach (VideoAndSubNames videoAndSubName in this.DataGridNames.Items)
            {
                videoAndSubName.SetNewSubtitle(suffix, applyRename);
            }

            this.DataGridNames.Items.Refresh();
        }

        private void AddVideoList(List<string> videoList)
        {
            if (videoList.Count == 0)
            {
                return;
            }

            this.DataGridNames.Items.Clear();
            foreach (string video in videoList)
            {
                this.DataGridNames.Items.Add(new VideoAndSubNames(video));
            }
        }

        private void AddSubtitleList(List<string> subtitleList)
        {
            if (subtitleList.Count == 0)
            {
                return;
            }

            if (this.DataGridNames.Items.Count != subtitleList.Count)
            {
                MessageBox.Show("The number of video files is not equal to that of subtitle files.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            for (int i = 0; i < subtitleList.Count; ++i)
            {
                VideoAndSubNames videoAndSubName = (VideoAndSubNames)this.DataGridNames.Items[i];
                videoAndSubName.SetSubtitle(subtitleList[i]);
            }
        }
    }
}
