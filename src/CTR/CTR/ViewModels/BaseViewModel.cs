using CTR.Infrastructure.Repository;

namespace CTR.ViewModels
{
    public abstract class BaseViewModel : NotifyErrorInfoViewModel
    {
        protected BaseViewModel(IRepository repository)
        {
            Repository = repository;
        }

        public IRepository Repository { get; }
    }
}
