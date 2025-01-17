﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management which can be executed in a transaction.
    /// </summary>
    public interface IItemsCommandService
    {
        /// <summary>
        /// Adds a new task to a project asynchronous. By default the task is added to the user’s Inbox project.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The temporary ID of the item.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(Item item);

        /// <summary>
        /// Closes a task asynchronous.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>
        /// A simplified version of item_complete / item_update_date_complete.
        /// The command does exactly what official clients do when you close a task: regular task is completed and moved to history,
        /// subtask is checked (marked as done, but not moved to history), recurring task is moved forward (due date is updated).
        /// </remarks>
        Task CloseAsync(ComplexId id);

        /// <summary>
        /// Completes a recurring task. See also <see cref="ItemsCommandService.CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="id">The recurring task ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task CompleteRecurringAsync(ComplexId id);

        /// <summary>
        /// Completes a recurring task. See also <see cref="CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeRecurringItemArgument">The complete recurring item argument.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeRecurringItemArgument"/> is <see langword="null"/></exception>
        Task CompleteRecurringAsync(CompleteRecurringItemArgument completeRecurringItemArgument);

        /// <summary>
        /// Deletes an existing task asynchronous.
        /// </summary>
        /// <param name="id">List of the IDs of the tasks to delete.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id);

        /// <summary>
        /// Uncompletes tasks and moves them to the active projects.
        /// </summary>
        /// <param name="id">The ids of the tasks.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UncompleteAsync(ComplexId id);

        /// <summary>
        /// Updates a task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Item item);

        /// <summary>
        /// Updates the day orders of multiple items at once.
        /// </summary>
        /// <param name="idsToOrder">The ids to order.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrder"/> is <see langword="null"/></exception>
        Task UpdateDayOrdersAsync(params OrderEntry[] idsToOrder);

        /// <summary>
        /// Moves task to a different location asynchronous.
        /// </summary>
        /// <param name="moveArgument">The move entry.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task MoveAsync(ItemMoveArgument moveArgument);

        /// <summary>
        /// Update the orders and indents of multiple items at once asynchronous.
        /// </summary>
        /// <param name="reorderEntries">The reorder entries.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderEntries" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentException">Value cannot be an empty collection.</exception>
        Task ReorderAsync(params ReorderEntry[] reorderEntries);

        /// <summary>
        /// Completes tasks and optionally move them to history. See also <see cref="ItemsCommandService.CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeItemArgument">The complete item argument.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeItemArgument"/> is <see langword="null"/></exception>
        Task CompleteAsync(CompleteItemArgument completeItemArgument);

        /// <summary>
        /// Archives a task and all its descendants asynchronous.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>
        /// It is mostly intended to allow users to force sub-tasks to be moved to the history since root tasks are automatically
        /// archived upon completion. See also item_close for a simplified version of the command.
        /// </remarks>
        Task ArchiveAsync(ComplexId id);

        /// <summary>
        /// UnArchives a task and all its descendants asynchronous.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>
        /// No ancestors will be restored from the history. Instead, the item is unarchived (and uncompleted) alone, loses any
        /// parent relationship (becomes a project root item), and is placed at the end of the list of other project root items.
        /// </remarks>
        Task UnArchiveAsync(ComplexId id);
    }
}
