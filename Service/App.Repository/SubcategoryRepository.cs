using App.DAL;
using App.Entity;

namespace App.Repository
{
    public class SubcategoryRepository : Repository<ProductSubcategory>, ISubcategoryRepository
    {
        private AdventureWorksEntities dataContext;

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public SubcategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        protected AdventureWorksEntities DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
    }

    public interface ISubcategoryRepository : IRepository<ProductSubcategory>
    {
    }
}
