using System;
using System.Collections.Generic;

namespace CTR.Infrastructure.Repository
{
    public interface IRepository
    {
        T Add<T>(T entity)
            where T : class;

        bool AddOrUpdate<T>(T entity)
            where T : class;

        T Delete<T>(T entity)
            where T : class;

        bool Delete<T>(Func<T, bool> predicate)
            where T : class;

        bool DeleteAll<T>()
            where T : class;

        T First<T>()
            where T : class;

        T First<T>(Func<T, bool> predicate)
            where T : class;

        IEnumerable<T> Get<T>(Func<T, bool> predicate)
            where T : class;

        IEnumerable<T> GetAll<T>()
            where T : class;
    }
}