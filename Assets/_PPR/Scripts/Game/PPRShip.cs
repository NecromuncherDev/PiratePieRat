using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRShip : PPRMonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        private bool _moving = false;
        private Vector2 _moveDir = Vector2.zero;

        private void Update()
        {
            if (_moving)
                Move(_moveDir);
        }

        protected void CheckStartMoving(object obj)
        {
            if (obj as string == ObjectID)
                ToggleMoving(true);  
        }

        protected void CheckStopMoving(object obj)
        {
            if (obj as string == ObjectID)
                ToggleMoving(false);
        }

        private void ToggleMoving(bool moving)
        {
            _moveDir = Random.insideUnitCircle.normalized;
            _moving = moving;
        }

        private void Move(Vector2 direction)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }
}