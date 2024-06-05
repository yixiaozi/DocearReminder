using System.Net.Http;
using System.Threading.Tasks;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Services
{
    /// <summary>
    /// Contains operations for Todoist log management.
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// Gets list of activity logs.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The activity log entries.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>The activity log is only available for Todoist Premium.</remarks>
        Task<Activity> GetAsync(LogFilter filter = null);
    }
}
