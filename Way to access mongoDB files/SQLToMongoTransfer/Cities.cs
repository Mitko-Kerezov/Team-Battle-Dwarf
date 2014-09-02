using System;
using MongoDB.Bson;

namespace SQLToMongoTransfer
{
    class Cities
    {
        public BsonObjectId Id { get; set; }
        public int CityID { get; set; }
        public string Name { get; set; }
        public int Edition { get; set; }
    }
}
