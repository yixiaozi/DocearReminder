using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Services
{
    internal class NotificationsService : NotificationsCommandService, INotificationsService
    {
        public NotificationsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        public NotificationsService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <summary>
        /// Gets all live notifications.
        /// </summary>
        /// <returns>List of the notifications.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Notification>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Notifications).ConfigureAwait(false);

            return response.Notifications;
        }
    }
}
