using CTR.Infrastructure.Repository;
using ReactiveUI;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace CTR.ViewModels
{
    public abstract class ViewModelBase : NotifyErrorInfoViewModel
    {
        public readonly IRepository Repository;
        public readonly DispatcherScheduler UiDispatcherScheduler;

        protected ViewModelBase(IRepository repository, DispatcherScheduler uiDispatcherScheduler)
        {
            Repository = repository;
            UiDispatcherScheduler = uiDispatcherScheduler;
        }

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
