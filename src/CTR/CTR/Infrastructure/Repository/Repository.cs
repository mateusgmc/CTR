using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace CTR.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly IReactiveRepository _reactiveRepository = new ReactiveRepository();

        public T Add<T>(T entity) where T : class
        {
            return _reactiveRepository.Add(entity).Wait();
        }

        public bool AddOrUpdate<T>(T entity) where T : class
        {
            return _reactiveRepository.AddOrUpdate(entity).Wait();
        }

        public T Delete<T>(T entity) where T : class
        {
            return _reactiveRepository.Delete(entity).Wait();
        }

        public bool Delete<T>(Func<T, bool> predicate) where T : class
        {
            return _reactiveRepository.Delete(predicate).Wait();
        }

        public bool DeleteAll<T>() where T : class
        {
            return _reactiveRepository.DeleteAll<T>().Wait();
        }

        public T First<T>() where T : class
        {
            return _reactiveRepository.First<T>().Wait();
        }

        public T First<T>(Func<T, bool> predicate) where T : class
        {
            return _reactiveRepository.First<T>(predicate).Wait();
        }

        public IEnumerable<T> Get<T>(Func<T, bool> predicate) where T : class
        {
            return _reactiveRepository.Get<T>(predicate).Wait();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _reactiveRepository.GetAll<T>().Wait();
        }
    }
}
