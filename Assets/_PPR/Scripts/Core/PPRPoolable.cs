namespace PPR.Core
{
    public class PPRPoolable : PPRMonoBehaviour
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
}
