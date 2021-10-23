using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Observer.Observers;

namespace Tank2021SharedContent.Observer.Subjects
{
    public abstract class Subject
    {
        public List<IObserver> Observers { get; set; }
        public abstract void AttatchObserver(IObserver observer);
        public abstract void DeattatchObserver(IObserver observer);
        public abstract void NotifyAll();
    }
}
