using System.Data.Entity.Validation;
using System.Diagnostics;
using App.Model;

namespace App.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private AdventureWorksEntities _dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        protected AdventureWorksEntities DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }

        public void Commit()
        {
            try
            {
                DataContext.SaveChanges();
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
    }
}
