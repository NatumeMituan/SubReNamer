using System.IO;

namespace SubReNamer
{
    public partial class MainWindow
    {
        public class VideoAndSubNames
        {
            public VideoAndSubNames(string videoPath, string subPath = "", string newSubPath = "")
            {
                this.VideoPath = videoPath;
                this.SubPath = subPath;
                this.NewSubPath = newSubPath;

                this.VideoName = Path.GetFileName(videoPath);
                this.SubName = Path.GetFileName(subPath);
                this.NewSubName = Path.GetFileName(newSubPath);
            }

            public string VideoPath { get; }

            public string SubPath { get; private set; }

            public string NewSubPath { get; private set; }

            public string VideoName { get; }

            public string SubName { get; private set; }

            public string NewSubName { get; private set; }


            public void SetSubtitle(string subPath)
            {
                this.SubPath = subPath;
                this.SubName = Path.GetFileName(subPath);
            }

            public void SetNewSubtitle(string suffix = "", bool applyRename = false)
            {
                this.GetNewSubtitle(suffix);

                if (!applyRename)
                {
                    return;
                }

                Utils.RenameFile(this.SubPath, this.NewSubPath);

                this.SetSubtitle(this.NewSubPath);
                this.SetNewSubtitle("");
            }

            private void SetNewSubtitle(string newSubPath)
            {
                this.NewSubPath = newSubPath;
                this.NewSubName = Path.GetFileName(newSubPath);
            }

            private void GetNewSubtitle(string suffix = "")
            {
                string? directory = Path.GetDirectoryName(this.SubPath);
                string extension = Path.GetExtension(this.SubPath);
                if (directory != null)
                {
                    this.SetNewSubtitle(
                        Path.Combine(
                            directory,
                            Path.GetFileNameWithoutExtension(this.VideoName) + suffix + extension));
                }
            }
        }
    }
}
