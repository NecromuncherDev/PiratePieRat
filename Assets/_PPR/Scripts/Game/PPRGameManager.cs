using PPR.Core;
using System;
using UnityEngine;

namespace PPR.Game
{
    public class PPRGameManager : PPRMonoBehaviour
    {
        [SerializeField] private PPRPlayerShip playerPrefab;

        public static string playerObjectID { get; private set; }

        private void OnEnable()
        {
            // AddListener to player_object_awake, keep ID
            AddListener(PPRCoreEvents.player_object_awake, NotePlayerObjectID);
        }

        private void OnDisable()
        {
            // RemoveListener from player_object_awake, discard ID
            AddListener(PPRCoreEvents.player_object_awake, DiscardPlayerObjectID);
        }

        private void Start()
        {
            // Instantiate Player
            Manager.FactoryManager.CreateAsync<PPRPlayerShip>(playerPrefab, Vector3.zero, PrintPlayer);
        }

        private void PrintPlayer(PPRPlayerShip obj)
        {
            print($"{obj} created");
        }

        private void NotePlayerObjectID(object obj)
        {
            playerObjectID = obj as string;
            print(playerObjectID);
        }

        private void DiscardPlayerObjectID(object obj)
        {
            playerObjectID = null;
        }
    }
}