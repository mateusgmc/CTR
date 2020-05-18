using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CTR.ViewModels
{
    public abstract class NotifyErrorInfoViewModel : BindableBase, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private readonly Dictionary<string, List<string>> _validationErrorsByProperty =
            new Dictionary<string, List<string>>();

        public virtual bool HasErrors => _validationErrorsByProperty.Any();

        public void AddError(string error, [CallerMemberName] string propertyName = "")
        {
            AddErrors(new List<string> { error }, propertyName);
        }

        public void AddErrors(List<string> errors, [CallerMemberName] string propertyName = "")
        {
            RemoveErrors(propertyName);
            _validationErrorsByProperty[propertyName] = errors;
            RaiseErrorsChanged(propertyName);
        }

        public void RemoveErrors([CallerMemberName] string propertyName = "")
        {
            _validationErrorsByProperty.Remove(propertyName);
            RaiseErrorsChanged(propertyName);
        }

        public virtual void RaiseErrorsChanged([CallerMemberName] string propertyName = "")
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _validationErrorsByProperty.Values;
            }
            if (!_validationErrorsByProperty.ContainsKey(propertyName))
            {
                return Enumerable.Empty<List<string>>();
            }
            return _validationErrorsByProperty[propertyName];
        }
    }
}
