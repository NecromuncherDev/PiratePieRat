using PPR.Core;
using System;
using UnityEngine;

namespace PPR.Game
{
    public class PPRSmoothFollow : PPRMonoBehaviour
    {
        [SerializeField] internal Transform target;
        [SerializeField, Range(0f, 10f)] private float dampening;

        private Vector2 targetPos, transformPos, newPos;
        private Vector2 velocity = Vector2.zero;

        private void LateUpdate()
        {
            if (target)
            {
                targetPos = target.position;
                transformPos = transform.position;

                newPos = Vector2.SmoothDamp(transformPos, targetPos, ref velocity, dampening);
                transform.Translate(newPos - transformPos);
            }
        }
    }
}