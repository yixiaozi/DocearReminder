using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Services
{
    /// <summary>
    /// Contains operations for labels management.
    /// </summary>
    /// <seealso cref="yixiaozi.API.Todoist.Services.ILabelsCommandService" />
    public interface ILabelsService : ILabelsCommandService
    {
        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>The labels.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Label>> GetAsync();

        /// <summary>
        /// Gets a label by ID.
        /// </summary>
        /// <param name="id">The ID of the label.</param>
        /// <returns>
        /// The label.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<LabelInfo> GetAsync(ComplexId id);
    }
}
