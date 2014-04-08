using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Server.Entity;

namespace Server.Services.DAL
{
    public class Disposable : IDisposable
    {
        private bool _isDisposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }
    }

    public interface IDatabaseFactory
    {
        AdventureWorksLT2012Entities Get();
        void Commit();
    }

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private static DatabaseFactory _instance = null;

        public static DatabaseFactory Instance
        {
            get { return _instance ?? (_instance = new DatabaseFactory()); }
        }

        private AdventureWorksLT2012Entities _dataContext;

        public AdventureWorksLT2012Entities Get()
        {
            return _dataContext ?? (_dataContext = new AdventureWorksLT2012Entities());
        }

        public void Commit()
        {
            try
            {
                _dataContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}", validationErrors.Entry.Entity.GetType().FullName,
                                      validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
