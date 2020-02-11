using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace BlockBreaker.Collision
{
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionController : MonoBehaviour,ICollisionnable
    {
        public CollisionType CollisionType = CollisionType.None;
        public IObservable<CollisionResult> CollisionStream => CollisionSubject;
        private readonly Subject<CollisionResult> CollisionSubject = new Subject<CollisionResult>();
        public IObservable<ContactPoint> ContactPointStream => ContactPointSubject;
        private readonly Subject<ContactPoint> ContactPointSubject = new Subject<ContactPoint>();
        private void Awake()
        {
            this.OnCollisionEnterAsObservable()
                .TakeUntilDestroy(gameObject)
                .Select(collision => new {col = collision,icol = collision.gameObject.GetComponent<ICollisionnable>()})
                .Where(n => n.icol != null)
                .Subscribe(n => {
                    ContactPointSubject.OnNext(n.col.GetContact(0));
                    CollisionSubject.OnNext(n.icol.OnCollisionEnter());
                });
        }

        public CollisionResult OnCollisionEnter()
        {
            return new CollisionResult(CollisionType);
        }
    }
    public interface ICollisionnable
    {
        CollisionResult OnCollisionEnter();
    }
    public class CollisionResult
    {
        public CollisionType CollisionType { get; }
        public CollisionResult(CollisionType collisionType)
        {
            CollisionType = collisionType;
        }
    }
    public enum CollisionType
    {
        None = 0, Wall, Block, Ball, Board
    }
}