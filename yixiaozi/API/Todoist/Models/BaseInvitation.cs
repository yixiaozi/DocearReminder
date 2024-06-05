using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    /// <summary>
    /// Represents a base invitation.
    /// </summary>
    internal class BaseInvitation : ICommandArgument
    {
        internal BaseInvitation(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("invitation_id")]
        public long Id { get; set; }
    }
}
