using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    /// <summary>
    /// Represents a Todoist backup.
    /// </summary>
    public class Backup
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty("url")]
        public string Url { get; internal set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        [JsonProperty("version")]
        public string Version { get; internal set; }
    }
}
