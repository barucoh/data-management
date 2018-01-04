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
using RawRabbit;

namespace DataManagement.Controllers
{
    [Route("api/[controller]")]
    public class DataManagementController : Controller
    {
        IBusClient rabbitMQClient;

        public DataManagementController(IBusClient rabbitMQClient)
        {
            this.rabbitMQClient = rabbitMQClient;
        }
        // GET api/DataManagement
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/DataManagement/<image-document-id>/<attachment-name>
        [HttpGet("{imageDocumentId}/{attachmentName}")]
        public async Task<string> Get(string imageDocumentId, string attachmentName)
        {
            string image = (await (GetImageDocument(imageDocumentId))).imageB64;
            string jsoned = JsonConvert.SerializeObject(image);
            return jsoned;
        }

        // POST api/DataManagement/UploadImage
        // Creating a new user
        [HttpPost]
        [Route("UploadImage")]
        public async Task<int> UploadImage([FromBody] Image image)
        {
            var httpClient = Helpers.CouchDBConnect.GetClient("users");
            string stringifiedJson =
                @"{" +
                "   '_id' : '" + image._id + "'," +
                "   'caption' : '" + image.caption + "'," +
                "   '_attachments' : " +
                "   {" +
                "      '" + image.name + "' : " +
                "      {" +
                "          'data' : '" + image.imageB64 + "'" +
                "      }" +
                "   }" +
                "}";
            var json = Newtonsoft.Json.Linq.JObject.Parse(stringifiedJson);
            HttpContent httpContent = new StringContent(
                json.ToString(),
                System.Text.Encoding.UTF8,
                "application/json"
                );
            var response = await httpClient.PostAsync("users", httpContent);
            await this.rabbitMQClient.PublishAsync(new
            {
                subject = "test",
                to = "spam",
                body = "this is a a test !!!"
            });
            Console.WriteLine(response);
            return 0;
        }

        // PUT api/DataManagement/5
        [HttpPut]
        [Route("UpdateImage")]
        public async void UpdateImage([FromBody]Image image)
        {
            Image img = await GetImageDocument(image._id);
            if (img != null)
            {
                var httpClient = Helpers.CouchDBConnect.GetClient("users");
                HttpContent httpContent = new ByteArrayContent(Convert.FromBase64String(image.imageB64));
                var response = await httpClient.PutAsync("users/" + image._id + "/" + image.name + "?rev=" + img._rev, httpContent);
                Console.WriteLine(response);
            }
        }

        // DELETE api/DataManagement/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public async Task<Image> GetImageDocument(string imageId)
        {
            var httpClient = Helpers.CouchDBConnect.GetClient("users");
            var response = await httpClient.GetAsync("users/" + imageId);
            Image img = (Image)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(Image));
            return response.IsSuccessStatusCode ? (Image)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(Image)) : null;
        }
    }
}
