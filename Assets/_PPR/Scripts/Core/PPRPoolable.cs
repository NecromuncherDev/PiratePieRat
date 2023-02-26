namespace PPR.Core
{
    public class PPRPoolable : PPRMonoBehaviour
    {
        public PoolNames poolName;

        /// <summary>
        /// Method <c>OnReturnedToPool</c> is called when a poolable object is returned to the pool.
        /// It sets the gameObject active=false
        /// </summary>
        public virtual void OnReturnedToPool()
        {
            this.gameObject?.SetActive(false);
        }

        /// <summary>
        /// Method <c>OnTakenFromPool</c> is called when a poolable object is taken from the pool.
        /// It sets the gameObject active=true
        /// </summary>
        public virtual void OnTakenFromPool()
        {
            this.gameObject?.SetActive(true);
        }

        public virtual void PreDestroy()
        {
        }
    }
}
