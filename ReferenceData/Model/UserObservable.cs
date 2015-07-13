using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData
{
    public class UserObservable : IObservable<UserFullInfo>
    {
        public UserObservable()
        {
            observers = new List<IObserver<UserFullInfo>>();
        }

        private List<IObserver<UserFullInfo>> observers;

        public IDisposable Subscribe(IObserver<UserFullInfo> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<UserFullInfo>> _observers;
            private IObserver<UserFullInfo> _observer;

            public Unsubscriber(List<IObserver<UserFullInfo>> observers, IObserver<UserFullInfo> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void FillData(UserFullInfo usr)
        {
            foreach (var observer in observers)
            {
                if (usr == null)
                    observer.OnError(new Exception());
                else
                    observer.OnNext(usr);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }
}
