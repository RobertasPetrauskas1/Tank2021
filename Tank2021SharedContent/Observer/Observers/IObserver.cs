using System.Threading.Tasks;

namespace Tank2021SharedContent.Observer.Observers
{
    public interface IObserver
    {
        public Task Update();
    }
}
