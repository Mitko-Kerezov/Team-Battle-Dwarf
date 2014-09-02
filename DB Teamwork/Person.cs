using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SQLToMongoTransfer
{
    public class Person
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int PersonID { get;set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
    }
}
