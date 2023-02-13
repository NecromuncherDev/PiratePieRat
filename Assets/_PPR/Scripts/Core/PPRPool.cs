using System.Collections.Generic;

namespace PPR.Core
{
    public class PPRPool
    {
        public Queue<PPRPoolable> AllPoolables = new();
        public Queue<PPRPoolable> UsedPoolables = new();
        public Queue<PPRPoolable> AvailablePoolables = new();

        public int MaxPoolables = 100;
    }
}
