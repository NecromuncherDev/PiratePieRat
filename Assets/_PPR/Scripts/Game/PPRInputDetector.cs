using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRInputDetector : PPRMonoBehaviour
    {
        [SerializeField] private float swipeThreshold = 20f;

        private Vector2 fingerDown;
        private Vector2 fingerUp;


        /// 1. Detect touch down
        /// 2. Detect touch up
        /// 3. If touch duration or length > swipe threshold
        ///     3.1. Swipe detected: InvokeEvent(PPREvents.player_object_start_move, moveDirVector2)
        /// 4. Else
        ///     4.1. Tap detected: InvokeEvent(PPREvents.player_object_stop_move, moveDirVector2)
        ///
        /// Do not detect swipe mid movement


        void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    OnTouchStart(touch.position);
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    OnTouchEnd(touch.position);
                }
            }
        }

        private Vector2 GetTouchTravelVector()
        {
            return (fingerUp - fingerDown);
        }

        private void OnTouchStart(Vector2 position)
        {
            fingerDown = position;
            fingerUp = fingerDown;
        }

        private void OnTouchEnd(Vector2 position)
        {
            fingerUp = position;

            Vector2 dir = GetTouchTravelVector();

            if (dir.magnitude > swipeThreshold)
                InvokeEvent(PPREvents.player_object_start_move, dir.normalized);
            else
                InvokeEvent(PPREvents.player_object_stop_move, dir.normalized);

        }
    }
}