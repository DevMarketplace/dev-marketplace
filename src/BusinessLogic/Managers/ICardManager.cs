using BusinessLogic.BusinessObjects;
using System;

namespace BusinessLogic.Managers
{
    public interface ICardManager
    {
        /// <summary>
        /// Creates a new task for a certain Card
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Guid CreateCardTask(TaskBo task);

        /// <summary>
        /// Updates an existing task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        TaskBo UpdateCardTask(TaskBo task);


        /// <summary>
        /// Deletes a task by Id
        /// </summary>
        /// <param name="id"></param>
        void DeleteCardTask(Guid id);

        /// <summary>
        /// Creates a new card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        Guid Create(CardBo card);

        /// <summary>
        /// Updates an existing card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        CardBo Update(CardBo card);

        /// <summary>
        /// Deletes an existing card
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);
    }
}
