using PPR.Core;
using System.Collections.Generic;
using UnityEngine;

namespace PPR.Test
{
    public class PPRTestSpawner : PPRMonoBehaviour
    {
        private Queue<PPRPoolable> poolables = new();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                var stranded = Manager.PoolManager.GetPoolable(PoolNames.RatStranded);
                poolables.Enqueue(stranded);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                var stranded = poolables.Dequeue();
                Manager.PoolManager.ReturnPoolable(stranded);
            }
        }
    }
}