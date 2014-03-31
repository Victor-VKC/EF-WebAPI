﻿using App.DAL;
using App.Model;

namespace App.Repository
{
    public class CustomerRepository: Repository<Customer>, ICustomerRepository
    {
        private AdventureWorksEntities dataContext;

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public CustomerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        protected AdventureWorksEntities DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
    }

    public interface ICustomerRepository: IRepository<Customer>
    {}
}
