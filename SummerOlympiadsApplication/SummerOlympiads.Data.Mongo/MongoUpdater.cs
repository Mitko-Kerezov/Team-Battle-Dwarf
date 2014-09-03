namespace SummerOlympiads.Data.Mongo
{
    using System.Linq;

    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    using SQLToMongoTransfer;
    using Model;

    public class MongoUpdater
    {
        private MongoDatabase mongoDb;

        public MongoUpdater(string connectionString = null)
        {
            if (connectionString == null)
            {
                connectionString = MongoSettings.Default.ConnectionString;
            }

            var mongoClient = new MongoClient(connectionString);
            var mongoServer = mongoClient.GetServer();
            this.mongoDb = mongoServer.GetDatabase(MongoSettings.Default.DatabaseName);
        }

        public void UpdateOlympiad(int year, string anthem)
        {
            var query = Query.EQ("Edition", year);
            var update = Update.Set("SpecialAnthem", anthem);
            var mongoCollection = this.mongoDb.GetCollection<SummerOlympiad>("Cities").Update(query, update);
        }
    }
}
