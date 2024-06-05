using System.Collections.Generic;

using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    internal class ReorderItemsArgument : ICommandArgument
    {
        public ReorderItemsArgument(IEnumerable<ReorderEntry> reorderArguments)
        {
            ReorderArguments = reorderArguments;
        }

        [JsonProperty("items")]
        public IEnumerable<ReorderEntry> ReorderArguments { get; }
    }
}
