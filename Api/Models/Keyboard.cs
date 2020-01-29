using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public partial class Keyboard
    {
        [JsonProperty("one_time")]
        public bool OneTime { get; set; }

        [JsonProperty("buttons")]
        public Button[][] Buttons { get; set; }
    }

    public partial class Button
    {
        [JsonProperty("action")]
        public Action Action { get; set; }

        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }
    }

    public partial class Action
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public string Payload { get; set; }

        [JsonProperty("app_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? AppId { get; set; }

        [JsonProperty("owner_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OwnerId { get; set; }

        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
    }
}
