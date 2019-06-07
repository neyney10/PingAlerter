using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Common
{
    /// <summary>
    /// Concrete Observable of generic type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Observable<T> : IObservable<T>
    {
        /// <summary>
        /// Set of Observers who subscribed to this Observable.
        /// </summary>
        private ISet<IObserver<T>> Observers = new HashSet<IObserver<T>>();

        /// <summary>
        /// Send notification to all subscribed observers.
        /// </summary>
        /// <param name="data"></param>
        public void NotifyObservers(T data)
        {
            foreach (IObserver<T> observer in Observers)
                observer.OnNext(data);
        }

        /// <summary>
        /// Subscribe an Observer to this Observable to recieve Push notifications.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns> Ubsubscriber : IDisposable </returns>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            this.Observers.Add(observer);
            return new Unsubscriber(this.Observers, observer);
        }

        #region Inner/Nested Unsubscriber class
        private class Unsubscriber : IDisposable
        {
            private ISet<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(ISet<IObserver<T>> observers, IObserver<T> observer)
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
        #endregion
    }
}
