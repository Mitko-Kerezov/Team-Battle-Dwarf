using System;
using MongoDB.Bson;

namespace SQLToMongoTransfer
{
    public class Person
    {
        public BsonObjectId Id { get; set; }
        public int AthletID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
    }
}
