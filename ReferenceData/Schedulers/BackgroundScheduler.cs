using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace ReferenceData
{
    public class BackgroundScheduler : IScheduler
    {
        public DateTimeOffset Now
        {
            get { return DateTimeOffset.UtcNow; }
        }

        public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            return TaskExecution(state, dueTime, action);
        }

        public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            return TaskExecution(state, new DateTimeOffset(this.Now.DateTime, dueTime), action);
        }

        public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action)
        {
            return TaskExecution(state, this.Now.DateTime, action);;
        }

        private IDisposable TaskExecution<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            IDisposable disposable = new SerialDisposable();
            
            var delay = dueTime - this.Now;

            if(delay == TimeSpan.Zero || delay <= TimeSpan.Zero)
                Task.Factory.StartNew(t => { disposable = action(this, state); }, TaskCreationOptions.LongRunning);
            else
                Task.Delay(delay).ContinueWith(t => { disposable = action(this, state); }, TaskContinuationOptions.LongRunning);

            return disposable;
        }
    }
}
