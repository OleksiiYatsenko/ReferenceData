using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReferenceData
{
    class MyDispatcherScheduler : IScheduler
    {
        private readonly Dispatcher dispatcher;

        public MyDispatcherScheduler(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
        public MyDispatcherScheduler()
        { }

        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }

        public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            return DispatcherExecute(state, dueTime, action);
        }

        public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            var offset = new DateTimeOffset(this.Now.UtcDateTime, dueTime);
            return DispatcherExecute(state, offset, action);
        }

        public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action)
        {
            return DispatcherExecute(state, this.Now.DateTime, action);
        }

        private IDisposable DispatcherExecute<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            IDisposable disposable = new SerialDisposable();

            var delay = dueTime - this.Now;

            if (delay == TimeSpan.Zero || delay <= TimeSpan.Zero)
                dispatcher.Invoke(new Action(() => { disposable = action(this, state); } ));
            else
                dispatcher.Invoke(new Action(() => { disposable = action(this, state); } ), TimeSpan.FromMilliseconds(dueTime.Millisecond));

            return disposable;
        }
    }
}
