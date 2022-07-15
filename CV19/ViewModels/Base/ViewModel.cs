using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName]string propName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            var handlers = PropertyChanged;
            if (handlers is null) return;

            var arg = new PropertyChangedEventArgs(propName);
            var invokation_list = handlers.GetInvocationList();
            foreach (var action in invokation_list)
            {
                if (action.Target is DispatcherObject disp)
                {
                    disp.Dispatcher.Invoke(action, this, arg);
                }
                else action.DynamicInvoke(this, arg);
            }
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propName= null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propName);
            return true;
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            var value_target_service = sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var root_object_sevice = sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            OnInitialized(value_target_service?.TargetObject, value_target_service?.TargetProperty, root_object_sevice?.RootObject);

            return this;
        }
        private WeakReference _TargetRef;
        private WeakReference _RootRef;
        public object TargetObject => _TargetRef?.Target;
        public object RootObject => _RootRef?.Target;
        protected virtual void OnInitialized(object target, object property, object root)
        {
            _TargetRef = new WeakReference(target);
            _RootRef = new WeakReference(root);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
        }
    }
}
