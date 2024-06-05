using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Services
{
    /// <summary>
    /// Contains operations for notes management.
    /// </summary>
    /// <seealso cref="yixiaozi.API.Todoist.Services.INotesCommandServices" />
    public interface INotesServices : INotesCommandServices
    {
        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>The notes.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Note>> GetAsync();

        /// <summary>
        /// Gets a note by ID.
        /// </summary>
        /// <param name="id">The ID of the note.</param>
        /// <returns>
        /// The note.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<NoteInfo> GetAsync(ComplexId id);
    }
}
