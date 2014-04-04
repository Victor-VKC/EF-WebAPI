using System;
using App.Entity;

namespace App.DAL
{
    public interface IDatabaseFactory: IDisposable
    {
        AdventureWorksEntities Get();
    }
}
