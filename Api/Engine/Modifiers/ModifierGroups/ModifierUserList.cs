using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierUserList : ModifierBaseKeywords
    {
        protected override List<string> Keywords => new List<string>
        {
            "список группы"
        };

        protected override bool CheckState(State state)
        {
            return state.Step == Step.AwaitAddNewAdmin;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            using var db = new DatabaseContext();
            var usersId = db.Users.Where(x => x.Group == state.User.Group).ToList();

            Task<string> result = GetUserInfo(usersId);
            var finalResult = result.Result;

            state.Step = Step.AwaitAddNewAdmin;

            return new ApiResponse
            {
                Text= finalResult+"\n\n" +
                "Введите Id пользователя",
                UserId= request.PeerId
            };
        }

        private async Task<string> GetUserInfo(List<User> users)
        {
            string url = $"https://api.vk.com/method/users.get/";


            List<String> onj = users.Select(u=> u.Id.ToString()).ToList();


            var stringUsers = string.Join(',', onj);

            using var client = new HttpClient();
            var data = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                {"user_ids",stringUsers},
                {"access_token",SecretKeys.SecretKey},
                {"v","5.103"},
                {"random_id",DateTime.Now.Millisecond.ToString()},
                });

            var response = await client.PostAsync(url, data);
            var contents = response.Content.ReadAsStringAsync().Result;

            var apiRequest = JsonConvert.DeserializeObject<InfoUser>(contents);

            var b = new List<string>();

            for (int i = 0; i < apiRequest.Response.Length; i++)
            {
                b.Add(i+1+". " +apiRequest.Response[i].FirstName + " | " + apiRequest.Response[i].LastName + " | " + apiRequest.Response[i].Id);
            }

            return string.Join('\n', b);
        }
    }
}
