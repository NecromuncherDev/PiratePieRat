using PPR.Core;
using System.Collections.Generic;
using UnityEngine;

namespace PPR.Test
{
    public class PPRTestSpawner : PPRMonoBehaviour
    {
        [SerializeField] private PoolNames poolName;
        private Queue<PPRPoolable> poolables = new();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var stranded = Manager.PoolManager.GetPoolable(poolName);
                poolables.Enqueue(stranded);
            }

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    var stranded = poolables.Dequeue();
            //    Manager.PoolManager.ReturnPoolable(stranded);
            //}
        }
    }
}