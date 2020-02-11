using System;
using UniRx;
namespace BlockBreaker.General
{
    public interface IGameInitializable
    {
        void Init(IObservable<Unit> moveStartObservable, IMoveInput moveInput,GameRuleHandler gameRule);
    }
}