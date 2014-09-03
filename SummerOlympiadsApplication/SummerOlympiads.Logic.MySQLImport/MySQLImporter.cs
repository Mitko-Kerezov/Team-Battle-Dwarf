namespace SummerOlympiads.Logic.MySQLImport
{
    using JSON.JSONImporter;
    using Model.MySQL;

    public class MySQLImporter
    {
        public void Import(IGoldMedalistsUnitOfWork mysqlDatabase)
        {
            var jsonImporter = new JSONImporter();
            var allMedalists = jsonImporter.GetAllRecords();
            

            foreach (var medalist in allMedalists)
            {
                mysqlDatabase.Add(medalist);
            }

            mysqlDatabase.SaveChanges();
        }

        
    }
}
