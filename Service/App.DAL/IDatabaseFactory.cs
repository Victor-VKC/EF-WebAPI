using System;
using App.Model;

namespace App.DAL
{
    public interface IDatabaseFactory: IDisposable
    {
        AdventureWorksEntities Get();
    }
}
