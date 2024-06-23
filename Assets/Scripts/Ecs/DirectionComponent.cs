using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct DirectionComponent
    {
        [HideInInspector]public Vector2 Input;
        public FixedJoystick Joystick;
    }
}