using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Repository;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue = false;
        private IDataContext _dataContext;
        private List<object> _repositories;

        public UnitOfWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
            _repositories = new List<object>();
        }

        public void AddRepository<TEntity>(IGenericRepository<TEntity> repository) where TEntity : class
        {
            _repositories.Add(repository);
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>(Type repository) where TEntity : class
        {
            return _repositories.First(x => x.GetType().FullName == repository.FullName) as IGenericRepository<TEntity>;
        }

        public void Submit()
        {
            using (var transaction = _dataContext.Database.BeginTransaction())
            {
                foreach(var repository in _repositories)
                {
                    try
                    {
                        (repository as IRepository).SubmitChanges();
                    }
                    catch(Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
