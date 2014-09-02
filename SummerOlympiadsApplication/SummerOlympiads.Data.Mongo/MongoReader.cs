namespace SummerOlympiads.Data.Mongo
{
    using System.Linq;

    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    using SQLToMongoTransfer;

    public class MongoReader
    {
        private readonly MongoDatabase mongoDb;

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

        public City GetCity(int edition)
        {
            var mongoCollection = this.mongoDb.GetCollection<City>("Cities");
            var query = Query.EQ("Edition", edition);
            var result = mongoCollection.Find(query).FirstOrDefault();
            return result;
        }

        public Event GetEvent(int eventId)
        {
            var mongoCollection = this.mongoDb.GetCollection<Event>("Events");
            var query = Query.EQ("EventId", eventId);
            var result = mongoCollection.Find(query).FirstOrDefault();
            return result;
        }

        public Person GetPerson(int personId)
        {
            var mongoCollection = this.mongoDb.GetCollection<Person>("Athletes");
            var query = Query.EQ("PersonID", personId);
            var result = mongoCollection.Find(query).FirstOrDefault();
            return result;
        }
    }
}