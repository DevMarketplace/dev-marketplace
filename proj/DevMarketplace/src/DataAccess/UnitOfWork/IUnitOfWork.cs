using DataAccess.Repository;
using System;
using System.Collections.Generic;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Adds a new repository to the unit of work.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        void AddRepository<TEntity>(IGenericRepository<TEntity> repository) where TEntity : class;

        /// <summary>
        /// Gets a repository from the unit of work by its type
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        IGenericRepository<TEntity> GetRepository<TEntity>(Type repository) where TEntity : class;

        /// <summary>
        /// Submits the changes of the repository
        /// </summary>
        void Submit();
    }
}
