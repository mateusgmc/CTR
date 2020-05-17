using System;
using System.Collections.Generic;

namespace CTR.Infrastructure.Repository
{
    public interface IRepository
    {
        IObservable<T> Add<T>(T entity)
            where T : class;

        IObservable<bool> AddOrUpdate<T>(T entity)
            where T : class;

        IObservable<T> Delete<T>(T entity)
            where T : class;

        IObservable<bool> Delete<T>(Func<T, bool> predicate)
            where T : class;

        IObservable<bool> DeleteAll<T>()
            where T : class;

        IObservable<T> First<T>()
            where T : class;

        IObservable<T> First<T>(Func<T, bool> predicate)
            where T : class;

        IObservable<IEnumerable<T>> Get<T>(Func<T, bool> predicate)
            where T : class;

        IObservable<IEnumerable<T>> GetAll<T>()
            where T : class;
    }
}
