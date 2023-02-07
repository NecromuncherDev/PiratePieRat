using PPR.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPR.Game
{
    public interface IPPRMobile
    {
        public float Speed { get; set; }
        public bool Moving { get; set; }

        public void Move(Vector2 direction);
        public void ToggleMoving(bool moving);
    }
}
