using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    internal class SyncResponse
    {
        [JsonProperty("sync_status")]
        public Dictionary<Guid, dynamic> SyncStatus { get; set; }

        [JsonProperty("temp_id_mapping")]
        public Dictionary<Guid, long> TempIdMappings { get; set; }
    }
}
