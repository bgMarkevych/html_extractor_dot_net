using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace html_exctractor.Common
{
    public class Utils
    {

        public static string GetStringResource(string name) => Application.Current.FindResource(name) as string;
        public static SolidColorBrush GetColorBrushResource(string name) => Application.Current.FindResource(name) as SolidColorBrush;

        public static void TryOpenFile(string filePath)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            try
            {
                process.StartInfo.FileName = filePath;
                process.Start();
            }
            catch
            {
                process.StartInfo.FileName = Path.GetDirectoryName(filePath);
                process.Start();
            }
        }

    }
}
