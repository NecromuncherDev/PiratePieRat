using PPR.Core;
using System.Threading.Tasks;

namespace PPR.Game
{
    public interface IPPRCombatant
	{
		public Task BeginAttack<T>(T opponent) where T : PPRMonoBehaviour, IPPRCombatant;
		public void Hit(int damage);
		public void StopAttack();
		public void AttackOnce(IPPRCombatant opponent);
	}
}