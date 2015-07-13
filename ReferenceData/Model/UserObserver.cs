using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData
{
    public class UserObserver : IObserver<UserFullInfo>
    {
        private IDisposable unsubscriber;
        private UserFullInfo inst;

        public UserObserver(UserFullInfo usr)
        {
            this.inst = usr;
        }

        public UserFullInfo Name
        { get { return this.inst; } }

        public virtual void Subscribe(IObservable<UserFullInfo> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            //Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            //Console.WriteLine("{0}: The location cannot be determined.", this.Name);
        }

        public virtual void OnNext(UserFullInfo value)
        {            
            value.FillSubdivisionsList();
            value.FillLocationList();
            //Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
}
