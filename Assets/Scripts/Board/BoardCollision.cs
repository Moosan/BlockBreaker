using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlockBreaker.Collision;
namespace BlockBreaker.Board
{
    public class BoardCollision : MonoBehaviour, ICollisionnable
    {
        public CollisionResult OnCollisionEnter()
        {
            return new CollisionResult(CollisionType.Board);
        }
    }
}
