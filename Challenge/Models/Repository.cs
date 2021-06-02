using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace challenge.Models

{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }


        [JsonPropertyName("created_at")]
        public string JsonDate { get; set; }

    }

    public class Owner
    {
        [JsonPropertyName("avatar_url")]
        public Uri GitAvatar_url { get; set; }
    }
}