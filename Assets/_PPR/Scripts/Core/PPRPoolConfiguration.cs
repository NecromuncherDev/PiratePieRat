using UnityEngine;

namespace PPR.Core
{
    [CreateAssetMenu(fileName = "Pool Config", menuName = "Configs/Pool Config", order = 1)]
    public class PPRPoolConfiguration : ScriptableObject
    {
        public PPRPoolable PoolableOriginial;
        public PoolNames PoolName;
        public int PoolInitialSize;
        public int PoolMaxSize;
    }
}