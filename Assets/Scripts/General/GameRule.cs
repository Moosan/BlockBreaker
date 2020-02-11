using System;
using UniRx;
namespace BlockBreaker.General
{
    public class GameRuleHandler
    {
        public ReactiveProperty<float> BoardSpeed { get; }
        public ReactiveProperty<float> BallSpeed { get; }
        public GameRuleHandler(GameRule gameRule)
        {
            BoardSpeed = new ReactiveProperty<float>(gameRule.BoardSpeed);
            BallSpeed = new ReactiveProperty<float>(gameRule.BallSpeed);
        }
    }
    [Serializable]
    public struct GameRule
    {
        public float BoardSpeed;
        public float BallSpeed;
    }
}