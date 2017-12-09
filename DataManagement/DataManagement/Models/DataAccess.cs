using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataManagement.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServerAddress _server;
        IMongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.Settings.Server;
            _db = _client.GetDatabase("EmployeeDB");
        }
    }
}
