using App.Entity;

namespace App.DAL
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private static DatabaseFactory _instance = null;

        public static DatabaseFactory Instance
        {
            get { return _instance ?? (_instance = new DatabaseFactory()); }
        }

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
