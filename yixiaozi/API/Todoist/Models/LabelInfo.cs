using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    /// <summary>
    /// Represents an information about a Todoist label.
    /// </summary>
    public class LabelInfo
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>The label.</value>
        [JsonProperty("label")]
        public Label Label { get; internal set; }
    }
}
