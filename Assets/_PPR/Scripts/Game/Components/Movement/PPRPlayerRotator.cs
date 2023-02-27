using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRPlayerRotator : PPRLogicMonoBehaviour
    {
        private void OnEnable()
        {
            AddListener(PPREvents.player_object_start_move, OnStartMoveInDir);
        }

        private void OnStartMoveInDir(object obj)
        {
            var moveDir = ((Vector2)obj).normalized;
            RotateToDirection(moveDir);
        }

        private void RotateToDirection(Vector2 dir)
        {
            transform.right = dir;
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.player_object_start_move, OnStartMoveInDir);
        }
    }
}