using System;
using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRControlledMovementComponent : PPRLogicMonoBehaviour
    {
        private float moveSpeed;
        private Vector2 moveDir;
        private bool isMoving = false;

        private void OnEnable()
        {
            AddListener(PPREvents.upgraded_movement_speed, SetSpeed);
            AddListener(PPREvents.player_object_start_move, StartMove);
            AddListener(PPREvents.player_object_stop_move, StopMove);
        }

        private void Start()
        {
            SetSpeed();
        }

        private void StartMove(object obj)
        {
            moveDir = (Vector2)obj;
            isMoving = true;
            //Debug.DrawLine(transform.position, transform.position + (Vector3)moveDir, Color.blue, 3f);
        }

        private void SetSpeed(object obj = null)
        {
            moveSpeed = GameLogic.UpgradeManager.GetCurrentValueByID(UpgradeableTypeIDs.MovementSpeed);
        }

        private void StopMove(object obj)
        {
            isMoving = false;
        }

        private void Move()
        {
            if (!isMoving)
                return;

            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        private void Update()
        {
            Move();
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.upgraded_movement_speed, SetSpeed);
            RemoveListener(PPREvents.player_object_start_move, StartMove);
            RemoveListener(PPREvents.player_object_stop_move, StopMove);
        }
    }
}