using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace DataAccess
{
    /// <summary>
    /// A basic IDataContext interface that provides an abstraction from the real EF DBContext.
    /// Helps with IoC instantiation
    /// </summary>
    public interface IDataContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entityToDelete) where TEntity : class;

        DatabaseFacade Database { get; }

        void SaveChanges();
    }
}
