using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Common
{
    class Observer<T> : IObserver<T>
    {
        private readonly Action<T> OnNextFunction;

        public Observer(Action<T> OnNext)
        {
            this.OnNextFunction = OnNext;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(T value)
        {
            this.OnNextFunction(value);
        }
    }
}
