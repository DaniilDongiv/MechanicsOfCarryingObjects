using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct MovableComponent
    {
         public Animator AnimatorController;
         public Rigidbody Rigidbody;
         public float Speed;
    }
}