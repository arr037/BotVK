using Api.Engine;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class ApiController:ControllerBase
    {
        private static readonly ConcurrentDictionary<long,UserSession> Sessions = new ConcurrentDictionary<long,UserSession>();
        private static readonly JsonSerializerSettings ConverterSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        [HttpGet]
        public string Get()
        {
            return "It works!";
        }

        [HttpPost]
        public IActionResult Post()
        {
            using var reader = new StreamReader(Request.Body);
            var body = reader.ReadToEnd();

            var apiRequest = JsonConvert.DeserializeObject<ApiRequest>(body, ConverterSettings);

            switch (apiRequest.Type)
            {
                case "confirmation":
                    return Ok(SecretKeys.ActivateCode);
                case "message_new":
                     NewMessage(apiRequest);
                    break;
            }

            return Ok("ok");
        }


        private void NewMessage(ApiRequest apiRequest)
        {
            var simpleApiRequest = new SimleApiRequest
            {
                Text = apiRequest.Object.Message.Text,
                FromId = apiRequest.Object.Message.FromId,
                PeerId = apiRequest.Object.Message.PeerId,
            };

            var userId = simpleApiRequest.FromId;

            var session = Sessions.GetOrAdd(userId, uid => new UserSession(uid));

            var apiResponse = session.HandleRequest(simpleApiRequest);
            SendMessage(apiResponse);
        }

        public async void SendMessage(ApiResponse response)
        {
            string url = "https://api.vk.com/method/messages.send/";
            
            using var client = new HttpClient();
            var data = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                {"message",response.Text},
                {"keyboard",response?.KeyBoard},
                {"peer_id",response.UserId.ToString()},
                {"access_token",SecretKeys.SecretKey},
                {"v","5.103"},
                {"random_id",DateTime.Now.Millisecond.ToString()},
                });
            

            await client.PostAsync(url, data);
        }
    }
}
