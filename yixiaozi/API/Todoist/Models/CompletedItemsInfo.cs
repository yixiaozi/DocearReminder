using System.Collections.Generic;

using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    /// <summary>
    /// Represents information about completed tasks.
    /// </summary>
    public class CompletedItemsInfo
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty("items")]
        public IReadOnlyCollection<CompletedItem> Items { get; internal set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        [JsonProperty("projects")]
        public IReadOnlyDictionary<long, Project> Projects { get; internal set; }
    }
}
