using System.Reactive.Concurrency;
using CTR.Infrastructure.Repository;

namespace CTR.ViewModels
{
    public class MainViewModel : ReactiveViewModelBase
    {
        public MainViewModel(IReactiveRepository repository, DispatcherScheduler uiDispatcherScheduler)
            : base(repository, uiDispatcherScheduler)
        {
        }
    }
}
