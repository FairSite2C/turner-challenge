using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;

namespace turner_challenge.Controllers
{
    public class Title{
        
    [BsonElement("TitleName")]
    public string TitleName {get;set;}
    }

    public class TitlesController : Controller
    {
        string host = "mongodb://readonly:turner@ds043348.mongolab.com:43348/dev-challenge";
        //string host="mongodb://turner@ds043348.mongolab.com:43348";
        string dbName = "dev-challenge";
        
        [HttpGet]
        [Route("api/titles/getTitles")]
        public async Task<List<string>> GetTitles(string searchTerm)
        {
            string collectionName = "Titles";

            var retVal = new List<string>();
            try
            {
                var database = getDatabase();
 
                var titles = database.GetCollection<BsonDocument>(collectionName);
                var builder = Builders<BsonDocument>.Filter;
                var filter1 = builder.Regex("TitleName",new BsonRegularExpression(searchTerm, "i"));

                using (var cursor = await titles.FindAsync(filter1))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        var batch = cursor.Current;
                        foreach (var document in batch)
                        {
                            retVal.Add(document["TitleName"].ToString());        
                        }
                    }
                }
 
       
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return retVal;
        }

        [HttpGet]
        [Route("api/titles/getDetail")]
        public async Task<string> GetDetail(string title)
        {
            string collectionName = "Titles";

            BsonDocument retVal = null;

            try
            {
                var database = getDatabase();
 
                var titles = database.GetCollection<BsonDocument>(collectionName);
                var builder = Builders<BsonDocument>.Filter;
                var filter1 = builder.Eq("TitleName",title);

                using (var cursor = await titles.FindAsync(filter1))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        var batch = cursor.Current;
                        foreach (var document in batch)
                        {
                            retVal = document;        
                        }
                    }
                }
 
       
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
   
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.JavaScript }; // key part
            
            return retVal.ToJson(jsonWriterSettings);
        }

        private IMongoDatabase getDatabase(){
            MongoClient client = new MongoClient( new MongoDB.Driver.MongoUrl(host));
            return client.GetDatabase(dbName);
        } 
    }
      
}
