using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PPR.Core
{
    public class PPRPoolManager
    {
        private Dictionary<PoolNames, PPRPool> Pools = new();

        private Transform rootPools;

        public PPRPoolManager()
        {
            rootPools = new GameObject("Pools Parent").transform;
            Object.DontDestroyOnLoad(rootPools);
        }

        public void InitPool(PPRPoolConfiguration poolConfig)
        {
            if (poolConfig.PoolableOriginial == null)
            {
                Debug.LogError("PPRPoolManager - Attempted to initialize a pool with null as original object.");
                return;
            }

            PPRManager.Instance.FactoryManager.MultiCreate(poolConfig.PoolableOriginial, Vector3.zero, poolConfig.PoolInitialSize,
                delegate (List<PPRPoolable> list)
                {
                    foreach (var poolable in list)
                    {
                        poolable.poolName = poolConfig.PoolName;
                        poolable.name = poolConfig.PoolableOriginial.name;
                        poolable.transform.parent = rootPools;
                        poolable.gameObject.SetActive(false);
                    }

                    var pool = new PPRPool
                    {
                        AllPoolables = new Queue<PPRPoolable>(list),
                        UsedPoolables = new Queue<PPRPoolable>(),
                        AvailablePoolables = new Queue<PPRPoolable>(list),
                        MaxPoolables = poolConfig.PoolMaxSize
                    };

                    Pools.Add(poolConfig.PoolName, pool);
                });
        }

        public PPRPoolable GetPoolable(PoolNames poolName)
        {
            if (Pools.TryGetValue(poolName, out PPRPool pool))
            {
                if (pool.AvailablePoolables.TryDequeue(out PPRPoolable poolable))
                {
                    //Debug.Log($"GetPoolable - {poolName}");

                    poolable.OnTakenFromPool();

                    pool.UsedPoolables.Enqueue(poolable);
                    poolable.gameObject.SetActive(true);
                    return poolable;
                }
                else if (pool.AllPoolables.Count < pool.MaxPoolables)
                {
                    // Create more
                }

                Debug.LogWarning($"pool - {poolName} not enough poolables, used poolables {pool.UsedPoolables.Count}");

                return null;
            }

            Debug.LogError($"pool - {poolName} wasn't initialized");
            return null;
        }


        public void ReturnPoolable(PPRPoolable poolable)
        {
            if (Pools.TryGetValue(poolable.poolName, out PPRPool pool))
            {
                pool.AvailablePoolables.Enqueue(poolable);
                poolable.OnReturnedToPool();
                poolable.gameObject.SetActive(false);
            }
        }


        public void DestroyPool(PoolNames name)
        {
            if (Pools.TryGetValue(name, out PPRPool pool))
            {
                foreach (var poolable in pool.AllPoolables)
                {
                    poolable.PreDestroy();
                    ReturnPoolable(poolable);
                }

                foreach (var poolable in pool.AllPoolables)
                {
                    Object.Destroy(poolable);
                }

                pool.AllPoolables.Clear();
                pool.AvailablePoolables.Clear();
                pool.UsedPoolables.Clear();

                Pools.Remove(name);
            }
        }
    }

    public enum PoolNames
    {
        NA = 0,
        ScoreToast = 1,
        PickupMetal = 2,
        PickupPlastic = 3,
        PickupWood = 4,
        PickupTrashHeap = 5, 
        // = 6
        MapMarker = 7,
    }
}
