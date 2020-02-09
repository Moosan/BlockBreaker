using System;
using UniRx;
namespace BlockBreaker.Manager
{
    public class TimeCountManager
    {
        public IObservable<int> CreateCountDownObservable(int CountTime)
        {
            return Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
                .Select(x => (int)(CountTime - x))
                .TakeWhile(x => x > 0);
        }
    }
}