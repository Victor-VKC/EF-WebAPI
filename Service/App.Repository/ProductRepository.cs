using App.DAL;
using App.Entity;

namespace App.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private static ProductRepository _instanse = null;

        public static ProductRepository Instanse
        {
            get { return _instanse ?? (_instanse = new ProductRepository(DAL.DatabaseFactory.Instance)); }
        }

        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
           
        }
    }
    public interface IProductRepository : IRepository<Product>
    {
    }
}
