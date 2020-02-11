using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using BlockBreaker.General;
namespace BlockBreaker.Board
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBoardMover : MonoBehaviour,IGameInitializable
    {
        private bool moveStart = false;
        private Rigidbody _rigidBody;
        private readonly ReactiveProperty<float> Speed = new ReactiveProperty<float>(1);
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
        public void Init(IObservable<Unit> moveStartObservable,IMoveInput moveInput,GameRuleHandler gameRule)
        {
            moveStartObservable
                .TakeUntilDestroy(gameObject)
                .Take(1)
                .Where(_ => !moveStart)
                .Subscribe(_ =>
                {
                    moveStart = true;
                    var moveStream = new Subject<float>();
                    var inputStream = this.UpdateAsObservable()
                        .TakeUntilDestroy(gameObject)
                        .WithLatestFrom(moveInput.MoveVertical.Timestamp(), (u, val) => val)
                        .Share();
                    inputStream
                        .Take(1)
                        .Select(t => t.Value)
                        .Subscribe(moveStream.OnNext)
                        .AddTo(gameObject);
                    inputStream
                        .TakeUntilDestroy(gameObject)
                        .Pairwise()
                        .Select(p => p.Previous.Timestamp != p.Current.Timestamp ? p.Current.Value : 0)
                        .Subscribe(moveStream.OnNext);
                    this.FixedUpdateAsObservable()
                        .TakeUntilDestroy(gameObject)
                        .WithLatestFrom(moveStream, (u, val) => val)
                        .Subscribe(Move);
                });
            gameRule
                .BoardSpeed
                .TakeUntilDestroy(gameObject)
                .Subscribe(SetSpeed);
            
        }
        private void SetSpeed(float speed)
        {
            Speed.Value = speed;
        }
        private void Move(float moveValue)
        {
            _rigidBody.velocity = Vector3.right * moveValue * Speed.Value;
        }
    }
}