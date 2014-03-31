using App.Model;

namespace App.DAL
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private AdventureWorksEntities _dataContext;
        public AdventureWorksEntities Get()
        {
            return _dataContext ?? (_dataContext = new AdventureWorksEntities());
        }
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
