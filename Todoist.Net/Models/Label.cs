using System;

using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;
using System.Collections.Generic;
namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist label.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Label : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - name</exception>
        public Label(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [JsonProperty("color")]
        public int Color { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonProperty("is_deleted")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the item order.
        /// </summary>
        /// <value>The item order.</value>
        [JsonProperty("item_order")]
        public int ItemOrder { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        public ICollection<KeyValuePair<string, string>> ToParameters()
        {
            var parameters = new LinkedList<KeyValuePair<string, string>>();

            parameters.AddLast(new KeyValuePair<string, string>("text", Name));
            parameters.AddLast(new KeyValuePair<string, string>("Color", "47"));
            parameters.AddLast(new KeyValuePair<string, string>("IsDeleted", "false"));
            parameters.AddLast(new KeyValuePair<string, string>("ItemOrder", "1"));

            return parameters;
        }
    }
}
