﻿using Newtonsoft.Json;

namespace SPT_AKI_Profile_Editor.Core.ProfileClasses
{
    public class ItemUpd
    {
        [JsonProperty("StackObjectsCount")]
        public long? StackObjectsCount { get; set; }

        [JsonProperty("Foldable")]
        public UpdFoldable Foldable { get; set; }

        [JsonProperty("Tag")]
        public Tag Tag { get; set; }

        [JsonProperty("SpawnedInSession")]
        public bool SpawnedInSession { get; set; }

        [JsonProperty("Dogtag")]
        public DogtagProperties Dogtag { get; set; }
    }
}