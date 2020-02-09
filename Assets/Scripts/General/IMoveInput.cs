using System;
namespace BlockBreaker.General
{
    public interface IMoveInput
    {
        IObservable<float> MoveVertical { get; }
    }
}