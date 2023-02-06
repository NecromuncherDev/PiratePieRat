using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PPR.Core
{
    public class PPRPoolManager
    {
        private Dictionary<string, PPRPool> Pools = new();

        public void InitPool(IPPRPoolable original, int amount, int maxAmount)
        {
            PPRManager.Instance.FactoryManager.MultiCreate(original, Vector3.zero, amount,
                delegate (List<IPPRPoolable> list)
                {
                    foreach (var poolable in list)
                    {
                        poolable.name = original.name;
                    }

                    var pool = new PPRPool
                    {
                        AllPoolables = new Queue<IPPRPoolable>(list),
                        UsedPoolables = new Queue<IPPRPoolable>(),
                        AvailablePoolables = new Queue<IPPRPoolable>(list),
                        MaxPoolables = maxAmount
                    };

                    Pools.Add(original.gameObject.name, pool);
                });
        }

        public IPPRPoolable GetPoolable(string poolName)
        {
            if (Pools.TryGetValue(poolName, out PPRPool pool))
            {
                if (pool.AvailablePoolables.TryDequeue(out IPPRPoolable poolable))
                {
                    poolable.OnTakenFromPool();

                    pool.UsedPoolables.Enqueue(poolable);
                    return poolable;
                }

                //Create more
                Debug.Log($"pool - {poolName} no enough poolables, used poolables {pool.UsedPoolables.Count}");

                return null;
            }

            Debug.Log($"pool - {poolName} wasn't initialized");
            return null;
        }


        public void ReturnPoolable(IPPRPoolable poolable)
        {
            if (Pools.TryGetValue(poolable.poolName, out PPRPool pool))
            {
                pool.AvailablePoolables.Enqueue(poolable);
                poolable.OnReturnedToPool();
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

    public class IPPRPoolable : PPRMonoBehaviour
    {
        public string poolName;

        public virtual void OnReturnedToPool()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void OnTakenFromPool()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void PreDestroy()
        {
        }
    }

    public class PPRPool
    {
        public Queue<IPPRPoolable> AllPoolables = new();
        public Queue<IPPRPoolable> UsedPoolables = new();
        public Queue<IPPRPoolable> AvailablePoolables = new();

        public int MaxPoolables = 100;
    }
}
