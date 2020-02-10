using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlockBreaker.General;
using System;
using UniRx;

namespace BlockBreaker.Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallMover : MonoBehaviour,IGameInitializable
    {
        private BallProvider BallProvider;
        private float ballSpeed = 0;
        private Rigidbody Rigidbody;
        private Vector3 Velocity;
        private void Awake()
        {
            BallProvider = GetComponent<BallProvider>();
            Rigidbody = GetComponent<Rigidbody>();
        }
        public void Init(IObservable<Unit> moveStartObservable, IMoveInput moveInput, GameRule gameRule)
        {
            moveStartObservable
                .TakeUntilDestroy(gameObject)
                .Take(1)
                .Subscribe(_ => {
                    gameRule
                        .BallSpeed
                        .TakeUntilDestroy(gameObject)
                        .Subscribe(SetBallSpeed);
                    Velocity = new Vector3(1,1,0).normalized;
                    BallProvider.CollisionController
                        .ContactPointStream
                        .TakeUntilDestroy(gameObject)
                        .Subscribe(contact => {
                            Velocity += 2 * Vector3.Dot(-Velocity, contact.normal) * contact.normal;
                        });
                    Observable.EveryFixedUpdate()
                        .TakeUntilDestroy(gameObject)
                        .Subscribe(__ => Rigidbody.velocity = Velocity.normalized * ballSpeed);
                });
        }
        private void SetBallSpeed(float ballSpeed)
        {
            this.ballSpeed = ballSpeed;
        }

    }
}