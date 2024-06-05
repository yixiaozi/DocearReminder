using System.Net.Http;
using System.Threading.Tasks;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Services
{
    /// <summary>
    /// Contains methods for sections management.
    /// </summary>
    /// <seealso cref="yixiaozi.API.Todoist.Services.ISectionsCommandService" />
    public interface ISectionService : ISectionsCommandService
    {
        /// <summary>
        /// Gets a section by ID.
        /// </summary>
        /// <param name="id">The ID of the section.</param>
        /// <returns>
        /// The section.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Section> GetAsync(ComplexId id);
    }
}
