using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlockBreaker.Collision;
namespace BlockBreaker.Wall
{
    public class WallCollision : MonoBehaviour, ICollisionnable
    {
        private CollisionResult CollisionResult = new CollisionResult(CollisionType.Wall);
        public CollisionResult OnCollisionEnter()
        {
            return CollisionResult;
        }
    }
}