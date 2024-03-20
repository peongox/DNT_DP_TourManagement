using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_TOURDL.Patterns.Observer
{
    public class HoaDonSubject 
    {
        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }
        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void Notify(string message)
        {
            foreach(var observer in  _observers)
            {
                observer.Update(message);
            }
        }
    }
}