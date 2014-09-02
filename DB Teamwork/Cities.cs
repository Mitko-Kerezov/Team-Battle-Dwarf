using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SQLToMongoTransfer
{
    class Cities
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int CityID { get; set; }
        public string Name { get; set; }
        public string SpecialAnthem { get; set; }
        public int Edition { get; set; }
    }
}
