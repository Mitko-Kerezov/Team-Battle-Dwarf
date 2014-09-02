namespace SummerOlympiads.ConsoleClient
{
    using SummerOlympiads.Logic.SqlImporter;
    using SummerOlympiads.Model;
    using SummerOlympiads.Utils;

    internal class EntryPoint
    {
        private static void Main()
        {
            using (var db = new OlympiadsEntities())
            {
                var sqlImporter = new SqlImporter();
                sqlImporter.Import(db);
            }

            ZipHandler.CleanUp();
        }
    }
}