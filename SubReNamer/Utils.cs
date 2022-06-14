using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SubReNamer
{
    internal static class Utils
    {
        private static readonly string[] videoExtensions = { ".mp4", ".mkv", ".avi" };
        private static readonly string[] subtitleExtensions = { ".ass", ".ssa", ".srt" };

        internal static void RenameFile(string oldName, string newName)
        {
            if (oldName == newName)
            {
                return;
            }

            if (File.Exists(newName))
            {
                File.Delete(newName);
            }

            File.Move(oldName, newName);
        }

        internal static void GetVideoAndSubList(List<string> fileList, out List<string> videoList, out List<string> subList)
        {
            videoList = new List<string>();
            subList = new List<string>();

            foreach (string file in fileList)
            {
                string ext = Path.GetExtension(file);
                if (videoExtensions.Contains(ext))
                {
                    videoList.Add(file);
                }
                else if (subtitleExtensions.Contains(ext))
                {
                    subList.Add(file);
                }
            }
        }
    }
}