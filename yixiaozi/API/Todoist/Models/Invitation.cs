using Newtonsoft.Json;

namespace yixiaozi.API.Todoist.Models
{
    internal class Invitation : BaseInvitation
    {
        internal Invitation(long id, string secret)
            : base(id)
        {
            Secret = secret;
        }

        [JsonProperty("invitation_secret")]
        public string Secret { get; set; }
    }
}
