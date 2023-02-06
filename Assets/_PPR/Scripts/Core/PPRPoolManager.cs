using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PPR.Core
{
    public class PPRPoolManager
    {
        private Dictionary<string, PPRPool> Pools = new();

        void InitPool<T>(T original, int amount, int maxAmount) where T : Object, IPPRPoolable
        {
            PPRManager.Instance.FactoryManager.MultiCreate<T>(original, Vector3.zero, amount, 
                delegate(List<T> list)
                {
                    var pool = new PPRPool
                    {
                        AllPoolables = new List<IPPRPoolable>(list),
                        UsedPoolables = new List<IPPRPoolable>(),
                        AvailablePoolables = new List<IPPRPoolable>(list),
                        MaxPoolables = maxAmount
                    };

                    Pools.Add(original.name, pool);
                });
        }

        public IPPRPoolable GetPoolable(string poolName)
        {
            
            if (Pools.TryGetValue(poolName, out var retPool))
            {
                
            }

            return null;
        }

        public void ReturnPoolable<T>(string poolName, T original) where T : Object, IPPRPoolable
        {

        }

        void DestroyPool()
        {
            
        }
    }

    public interface IPPRPoolable
    {
        
    }

    public class PPRPool 
    {
        public List<IPPRPoolable> AllPoolables = new();
        public List<IPPRPoolable> UsedPoolables = new();
        public List<IPPRPoolable> AvailablePoolables = new();

        public int MaxPoolables = 100;
    }
}
