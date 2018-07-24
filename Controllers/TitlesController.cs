using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using System.Configuration;
 
namespace turner_challenge.Controllers
{
    public class TitlesController : Controller
    {
        [HttpGet]
        [Route("api/titles/getTitles")]
        public List<string> GetTitles()
        {
              Database database = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();
 
            return new List<string> { "Gone Wih Wind", "value2" };
        }

    }
      
}
