﻿using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Rocket.Player.Interfaces
{
    [DataContract]
    public class PlayerMetadata
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id", DefaultValueHandling = DefaultValueHandling.Include)]
        public int ID { get; set; }

        [DataMember(Name = "color")]
        [JsonProperty(PropertyName = "color", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Color { get; set; }

        [DataMember(Name = "player")]
        [JsonProperty(PropertyName = "player", DefaultValueHandling = DefaultValueHandling.Include)]
        public Player Player { get; set; }
    }
}
