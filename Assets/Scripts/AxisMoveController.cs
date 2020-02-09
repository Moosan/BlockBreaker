using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
namespace BlockBreaker
{
    public class AxisMoveController : MonoBehaviour, IMoveInput
    {
        public IObservable<float> MoveVertical => VerticalArrowInput;
        private readonly Subject<float> VerticalArrowInput = new Subject<float>();
        void Awake()
        {
            this.UpdateAsObservable()
                .TakeUntilDestroy(gameObject)
                .Select(_ => Input.GetAxis("Horizontal"))
                .Subscribe(VerticalArrowInput.OnNext);
        }
    }
}