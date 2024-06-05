namespace yixiaozi.API.Todoist.Services
{
    /// <summary>
    /// Contains methods for sharing management.
    /// </summary>
    /// <seealso cref="yixiaozi.API.Todoist.Services.SharingCommandService" />
    internal class SharingService : SharingCommandService, ISharingService
    {
        public SharingService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }
    }
}
