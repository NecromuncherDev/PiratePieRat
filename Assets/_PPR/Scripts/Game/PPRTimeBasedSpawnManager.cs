using PPR.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace PPR.Game // TODO: Move to Core.Test
{
    public class PPRTimeBasedSpawnManager : PPRMonoBehaviour
    {
        [SerializeField] private List<PoolNames> pickupPools = new();
        [SerializeField] private float spawnInterveal = 5f;
        [SerializeField] private float spawnRadiusMin = 3f;
        [SerializeField] private float spawnRadiusMax = 3f;
        [SerializeField] private int maxSpawned = 15;
        
        private int currentSpawned = 0;

        CancellationTokenSource tokenSource = new();
        CancellationToken ct;

        /// 1. OnEnable Listen to on_game_start
        /// 2. When the game starts, start spawning randomly on interval from to pools up to a max
        /// 3. On game stop, stop spawning using cancellation token

        private void OnEnable()
        {
            AddListener(PPREvents.game_start_event, StartSpawning); // Possible loading related bug
        }

        private async void StartSpawning(object obj)
        {
            ct = tokenSource.Token;
            AddListener(PPREvents.game_stop_event, StopSpawning);
            AddListener(PPREvents.pickup_collected, OnPickupCollected);

            while (!ct.IsCancellationRequested)
            {
                SpawnRandomPickup();
                await Task.Delay(spawnInterveal.SecToMilli());
            }
        }

        private void OnPickupCollected(object obj)
        {
            var collectedPickup = (PPRPoolable)obj;

            if (pickupPools.Contains(collectedPickup.poolName))
                if (currentSpawned > 0)
                    currentSpawned--;
        }

        private void SpawnRandomPickup()
        {
            if (currentSpawned < maxSpawned)
            {
                // Spawn from random pool in pickupPools
                var stranded = Manager.PoolManager.GetPoolable(pickupPools[Random.Range(0, pickupPools.Count)]);
                stranded.gameObject.transform.position = GeneratePickupSpawnPoint();
                InvokeEvent(PPREvents.pickup_created, stranded.gameObject);
                currentSpawned++;
            }
        }

        private Vector3 GeneratePickupSpawnPoint()
        {
            var pos = (Camera.main.transform.position) -
                      (Vector3.forward * Camera.main.transform.position.z) +
                      (Vector3)Random.insideUnitCircle.normalized * Random.Range(spawnRadiusMin, spawnRadiusMax);
            return pos;
        }

        private void StopSpawning(object obj)
        {
            tokenSource.Cancel(); // Should stop spawning
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.game_start_event, StartSpawning);
            RemoveListener(PPREvents.game_stop_event, StopSpawning);
        }
    }
}