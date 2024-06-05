using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Services
{
    /// <summary>
    /// Contains methods for sections management.
    /// </summary>
    /// <seealso cref="yixiaozi.API.Todoist.Services.ISectionService" />
    /// <seealso cref="yixiaozi.API.Todoist.Services.SectionsCommandService" />
    internal class SectionService : SectionsCommandService, ISectionService
    {
        internal SectionService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <summary>
        /// Gets a section by ID.
        /// </summary>
        /// <param name="id">The ID of the section.</param>
        /// <returns>
        /// The section.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<Section> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<Section>(
                "sections/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("section_id", id.ToString())
                    });
        }
    }
}
