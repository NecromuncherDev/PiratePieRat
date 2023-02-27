using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRFollowPlayer : PPRSmoothFollow
    {
        private void OnEnable()
        {
            AddListener(PPREvents.player_object_awake, AssignPlayerTransform);
        }

        private void AssignPlayerTransform(object obj)
        {
            target = ((GameObject)obj).transform;
        }

        private void OnDisable()
        {
            AddListener(PPREvents.player_object_awake, AssignPlayerTransform);
        }
    }
}