using System;
using UniRx;
namespace BlockBreaker.General
{
    public class GameRule
    {
        public ReactiveProperty<float> BoardSpeed { get; }
        public ReactiveProperty<float> BallSpeed { get; }
        public GameRule(float boardSpeed,float ballSpeed)
        {
            BoardSpeed = new ReactiveProperty<float>(boardSpeed);
            BallSpeed = new ReactiveProperty<float>(ballSpeed);
        }
    }
}