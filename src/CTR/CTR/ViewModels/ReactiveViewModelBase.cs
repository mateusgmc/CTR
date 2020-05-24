using CTR.Infrastructure.Repository;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace CTR.ViewModels
{
    public abstract class ReactiveViewModelBase : NotifyErrorInfoViewModel
    {
        protected ReactiveViewModelBase(IReactiveRepository repository, DispatcherScheduler uiDispatcherScheduler)
        {
            Repository = repository;
            UiDispatcherScheduler = uiDispatcherScheduler;
        }

        public IReactiveRepository Repository { get; }
        public DispatcherScheduler UiDispatcherScheduler { get; }

        public int ObservingCount { get; set; }

        public bool IsBusy
        {
            get => ObservingCount >= 1;
        }

        protected IDisposable ObservarErroCampoObrigatorio(IObservable<bool> observable, string propertyName)
        {
            return
                observable
                    .Skip(1)
                    .Subscribe(hasErros =>
                    {
                        if (hasErros)
                        {
                            AddError("Esse campo é obrigatório.", propertyName);
                        }
                        else
                        {
                            RemoveErrors(propertyName);
                        }
                    });
        }
    }
}
