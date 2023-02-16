using PPR.Core;

namespace PPR.Game
{
    public class PPRLogicMonoBehaviour : PPRMonoBehaviour
    {
        public PPRGameLogic GameLogic => PPRGameLogic.Instance;
    }
}