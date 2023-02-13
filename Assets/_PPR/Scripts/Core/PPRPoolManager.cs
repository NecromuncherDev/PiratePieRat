using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PPR.Core
{
    public class PPRPoolManager
    {
        private Dictionary<string, PPRPool> Pools = new();

        private Transform rootPools;

        public PPRPoolManager()
        {
            rootPools = new GameObject().transform;
            Object.DontDestroyOnLoad(rootPools);
        }

        public void InitPool(PPRPoolable original, int amount, int maxAmount)
        {
            PPRManager.Instance.FactoryManager.MultiCreate(original, Vector3.zero, amount,
                delegate (List<PPRPoolable> list)
                {
                    foreach (var poolable in list)
                    {
                        poolable.name = original.name;
                        poolable.transform.parent = rootPools;
                        poolable.gameObject.SetActive(false);
                    }

                    var pool = new PPRPool
                    {
                        AllPoolables = new Queue<PPRPoolable>(list),
                        UsedPoolables = new Queue<PPRPoolable>(),
                        AvailablePoolables = new Queue<PPRPoolable>(list),
                        MaxPoolables = maxAmount
                    };

                    Pools.Add(original.poolName, pool);
                });
        }

        public PPRPoolable GetPoolable(string poolName)
        {
            if (Pools.TryGetValue(poolName, out PPRPool pool))
            {
                if (pool.AvailablePoolables.TryDequeue(out PPRPoolable poolable))
                {
                    Debug.Log($"GetPoolable - {poolName}");

                    poolable.OnTakenFromPool();

                    pool.UsedPoolables.Enqueue(poolable);
                    poolable.gameObject.SetActive(true);
                    return poolable;
                }

                //Create more
                Debug.Log($"pool - {poolName} not enough poolables, used poolables {pool.UsedPoolables.Count}");

                return null;
            }

            Debug.Log($"pool - {poolName} wasn't initialized");
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


        public void DestroyPool(string name)
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
}
