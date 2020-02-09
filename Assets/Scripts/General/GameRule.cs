using System;
using UniRx;
namespace BlockBreaker.General
{
    public class GameRule
    {
        public ReactiveProperty<float> BoardSpeed { get; }
        public GameRule(float boardSpeed)
        {
            BoardSpeed = new ReactiveProperty<float>(boardSpeed);
        }
    }
}