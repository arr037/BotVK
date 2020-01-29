using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public partial class InfoUser
    {
        [JsonProperty("response")]
        public Response[] Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("can_access_closed")]
        public bool CanAccessClosed { get; set; }
    }
}
