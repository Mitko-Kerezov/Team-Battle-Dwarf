namespace SummerOlympiads.Utils
{
    using System;
    using System.IO;

    using Ionic.Zip;

    public class ZipHandler
    {
        public static void ExtractFile(string sourceFolderPath, string destinationFolderPath)
        {
            try
            {
                using (var archives = ZipFile.Read(sourceFolderPath))
                {
                    foreach (var archive in archives)
                    {
                        archive.Extract(destinationFolderPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ZipFolder(string folderPath, string destinationFolderPath)
        {
            using (var archive = new ZipFile())
            {
                foreach (var directory in Directory.GetDirectories(folderPath))
                {
                    archive.AddDirectory(directory);
                }

                archive.Save(destinationFolderPath);
            }
        }

        public static string[] ExtractDefaultFile(string filename = null)
        {
            if (filename == null)
            {
                filename = ZipSettings.Default.SourceDataPath;
            }

            var archivesFolder = Path.GetTempPath() + ZipSettings.Default.TempFolder;
            ExtractFile(filename, archivesFolder);

            var filenames = Directory.GetFiles(archivesFolder, "*.xlsx");

            return filenames;
        }

        public static void CleanUp()
        {
            Directory.Delete(Path.GetTempPath() + ZipSettings.Default.TempFolder, true);
        }
    }
}