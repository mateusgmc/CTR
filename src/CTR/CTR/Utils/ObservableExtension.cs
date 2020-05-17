using CTR.ViewModels;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace CTR.Utils
{
    public static class ObservableExtension
    {
        public static IObservable<T> Busy<T>(this IObservable<T> observable, ViewModelBase viewModel, IScheduler scheduler)
        {
            var stream = Observable.Create<T>(obs =>
            {
                viewModel.ObservingCount++;
                var disposables = new CompositeDisposable
                {
                    observable
                        .SubscribeOn(scheduler)
                        .Subscribe(e => obs.OnNext(e), ex => obs.OnError(ex), () => obs.OnCompleted()),

                    Disposable.Create(() =>
                    {
                        viewModel.ObservingCount--;
                    })
                };
                return disposables;
            });
            return stream.SubscribeOn(viewModel.UiDispatcherScheduler);
        }

        public static IObservable<T> Busy<T>(this IObservable<T> observable, ViewModelBase viewModel)
            => Busy(observable, viewModel, NewThreadScheduler.Default);
    }
}
