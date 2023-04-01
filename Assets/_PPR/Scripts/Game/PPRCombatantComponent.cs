using DG.Tweening;
using PPR.Core;
using System.Threading.Tasks;
using UnityEngine;

namespace PPR.Game
{
    public class PPRCombatantComponent : PPRMonoBehaviour, IPPRCombatant
    {
        [SerializeField] private float attackCooldown = 1.5f;
        [SerializeField] private int attackPower = 1;

        [SerializeField] private int TEMP_hp = 5;

        private Vector3 attackTarget;
        private bool isAttacking = false;

        public async Task BeginAttack<T>(T opponent) where T : PPRMonoBehaviour, IPPRCombatant
        {
            attackTarget = opponent.transform.position;
            await FaceTarget(attackTarget);

            while (isAttacking )
            {
                AttackOnce(opponent);
                await Task.Delay(attackCooldown.SecToMilli());
            }
        }

        private async Task FaceTarget(Vector3 dir)
        {
            transform.DOLookAt2D(dir, 0.5f);
        }

        public void StopAttack()
        {
            isAttacking = false;
        }

        public void Hit(int damage)
        {
            PPRDebug.Log($"Taken {damage}");

            TEMP_hp -= damage;
            if (TEMP_hp <= 0)
            {
                StopAttack();
            }
        }

        public void AttackOnce(IPPRCombatant opponent)
        {
            // Trigger attack animations etc.
            // await AttackAnimToEnd()
            opponent.Hit(attackPower);
        }
    }
}