namespace PPR.Core
{
    public class PPRameLoader : PPRMonoBehaviour
    {
        private void Start()
        {
            // TODO: Use different Design Pattern
            Invoke("DelayStart", 0.1f);
        }

        private void DelayStart()
        {
            new PPRManager();
            Destroy(this.gameObject);
        }
    }
}