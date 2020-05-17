using System.Reactive.Concurrency;
using CTR.Infrastructure.Repository;

namespace CTR.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IRepository repository, DispatcherScheduler uiDispatcherScheduler)
            : base(repository, uiDispatcherScheduler)
        {
        }
    }
}
