using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataManagement.Helpers;
using DataManagement.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace DataManagement.Controllers
{
    [Route("api/[controller]")]
    public class DataManagementController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login/createuser
        // Creating a new user
        [HttpPost]
        [Route("UploadImage")]
        public async Task<int> UploadImage([FromBody] Image image)
        {
            var httpClient = Helpers.CouchDBConnect.GetClient("users");
            string jsonifiedImageObject = JsonConvert.SerializeObject(
                new
                {
                    _id = image.userId,
                    caption = image.caption,
                    _attachments = new
                    {
                        image.caption = new
                        {
                            data = image.imageB64
                        }
                    }
                });
                
            //var json = Newtonsoft.Json.Linq.JObject.Parse(jsonifiedImageObject);
            //((Newtonsoft.Json.Linq.JObject)((Newtonsoft.Json.Linq.JObject)json.GetValue("_attachments")).GetValue("image")).Remove("_rev");

            HttpContent httpContent = new StringContent(
                jsonifiedImageObject,
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await httpClient.PostAsync("users", httpContent);
            Console.WriteLine(response);
            return 0;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
