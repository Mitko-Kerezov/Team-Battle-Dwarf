namespace SummerOlympiads.Data.Mongo
{
    using MongoDB.Driver;

    public class MongoReader
    {
        private MongoDatabase mongoDb;

        public MongoReader(string connectionString = null)
        {
            if (connectionString == null)
            {
                connectionString = MongoSettings.Default.ConnectionString;
            }

            var mongoClient = new MongoClient(connectionString);
            var mongoServer = mongoClient.GetServer();
            this.mongoDb = mongoServer.GetDatabase(MongoSettings.Default.DatabaseName);
        }

        private void ReadRow()
        {
        }
    }
}