using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace CTR.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        public virtual IObservable<T> Add<T>(T entity)
            where T : class
        {
            return Observable.Create<T>(obs =>
            {
                using(var context = ContextFactory.Create())
                {
                    var ret = context.Set<T>().Add(entity);
                    context.SaveChanges();
                    obs.OnNext(ret);
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<bool> AddOrUpdate<T>(T entity)
            where T : class
        {
            return Observable.Create<bool>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    context.Set<T>().AddOrUpdate(entity);
                    obs.OnNext(context.SaveChanges() > 0);
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<T> Delete<T>(T entity)
            where T : class
        {
            return Observable.Create<T>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    var ret = context.Set<T>().Remove(entity);
                    obs.OnNext(ret);
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<bool> Delete<T>(Func<T, bool> predicate)
            where T : class
        {
            return Observable.Create<bool>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    context.Set<T>().Where(predicate).ToList().ForEach(p => context.Set<T>().Remove(p));
                    obs.OnNext(context.SaveChanges() > 0);
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<bool> DeleteAll<T>()
            where T : class
        {
            return Observable.Create<bool>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    context.Set<T>().ToList().ForEach(p => context.Set<T>().Remove(p));
                    obs.OnNext(context.SaveChanges() > 0);
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<T> First<T>()
            where T : class
        {
            return Observable.Create<T>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    obs.OnNext(context.Set<T>().FirstOrDefault());
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<T> First<T>(Func<T, bool> predicate)
            where T : class
        {
            return Observable.Create<T>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    obs.OnNext(context.Set<T>().Where(predicate).FirstOrDefault());
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<IEnumerable<T>> Get<T>(Func<T, bool> predicate)
            where T : class
        {
            return Observable.Create<IEnumerable<T>>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    obs.OnNext(context.Set<T>().Where(predicate).ToList());
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }

        public virtual IObservable<IEnumerable<T>> GetAll<T>()
            where T : class
        {
            return Observable.Create<IEnumerable<T>>(obs =>
            {
                using (var context = ContextFactory.Create())
                {
                    obs.OnNext(context.Set<T>().ToList());
                    obs.OnCompleted();
                }
                return Disposable.Empty;
            });
        }
    }
}
