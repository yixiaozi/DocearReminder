﻿using System.Collections.Generic;

using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    internal class ProjectReorderArgument : ICommandArgument
    {
        public ProjectReorderArgument(IEnumerable<ReorderEntry> reorderEntries)
        {
            ReorderEntries = reorderEntries;
        }

        [JsonProperty("projects")]
        public IEnumerable<ReorderEntry> ReorderEntries { get; }
    }
}
