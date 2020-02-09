using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
namespace BlockBreaker.Board
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBoardMover : MonoBehaviour
    {
        private bool moveStart = false;
        private Rigidbody _rigidBody;
        private ReactiveProperty<float> Speed = new ReactiveProperty<float>(1);
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
        public void Init(IObservable<Unit> moveStartObservable,IMoveInput moveInput)
        {
            moveStartObservable
                .TakeUntilDestroy(gameObject)
                .Take(1)
                .Where(_ => !moveStart)
                .Subscribe(_ =>
                {
                    moveStart = true;
                    var inputStream = this.UpdateAsObservable()
                        .TakeUntilDestroy(gameObject)
                        .WithLatestFrom(moveInput.MoveVertical.Timestamp(), (u, val) => val)
                        .Share();
                    inputStream
                        .Take(1)
                        .Select(t => t.Value)
                        .Subscribe(Move)
                        .AddTo(gameObject);
                    inputStream
                        .Pairwise()
                        .Select(p => p.Previous.Timestamp != p.Current.Timestamp ? p.Current.Value : 0)
                        .Subscribe(Move)
                        .AddTo(gameObject);
                });
            
        }
        public void SetSpeed(float speed)
        {
            Speed.Value = speed;
        }
        private void Move(float moveValue)
        {
            _rigidBody.velocity = Vector3.right * moveValue * Speed.Value;
        }
    }
    public interface IMoveInput
    {
        IObservable<float> MoveVertical { get; }
    }
}