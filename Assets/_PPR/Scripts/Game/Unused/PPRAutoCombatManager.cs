using PPR.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PPR.Game
{
    public class PPRAutoCombatManager
	{
		private PPRCombatantComponent player;
		private PPRCombatantComponent enemy;
		private bool isCombatActive;

		private List<Task> attackTasks = new();
		private int winnerTaskIndex;

		// Constructor
		public PPRAutoCombatManager()
		{
			PPRManager.Instance.EventManager.AddListener(PPREvents.enemy_encountered, OnEnemyEncountered);
		}

		private void OnEnemyEncountered(object combatData)
		{
			// combatData should be tuple (IPPRCombatParticipant, IPPRCombatParticipant)
			var combatants = ((PPRCombatantComponent, PPRCombatantComponent))combatData;
			player = combatants.Item1;
			enemy = combatants.Item2;

			StartCombat();		
		}

        private async void StartCombat()
        {
            if (player != null && enemy != null)
            {
				isCombatActive = true;
				await PlayCombat();
            }
        }

        private async Task PlayCombat()
        {
			attackTasks = new();
            while (isCombatActive)
            {
				attackTasks.Add(player.BeginAttack(enemy)); // Index 0
				attackTasks.Add(enemy.BeginAttack(player)); // Index 1
				
				winnerTaskIndex = Task.WaitAny(attackTasks.ToArray()); // Winner is the one who lasts longer

				if (winnerTaskIndex == 0)
				{
					// Enemy Win
					PPRManager.Instance.EventManager.InvokeEvent(PPREvents.combat_player_lose);
				}
				else 
				{
					// Player Win
					PPRManager.Instance.EventManager.InvokeEvent(PPREvents.combat_player_win);
				}

				player.StopAttack();
				enemy.StopAttack();

				isCombatActive = false;
			}
        }

        ~PPRAutoCombatManager()
		{
			PPRManager.Instance.EventManager.RemoveListener(PPREvents.enemy_encountered, OnEnemyEncountered);
		}
	}
}