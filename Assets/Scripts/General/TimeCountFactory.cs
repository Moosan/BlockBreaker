using System;
using UniRx;
namespace BlockBreaker.General
{
    public class TimeCountFactory
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