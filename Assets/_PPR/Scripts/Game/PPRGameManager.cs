using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRGameManager : PPRMonoBehaviour
    {
        public static string playerObjectID { get; private set; }
        [SerializeField] private PPRPlayerShip playerPrefab;

        private void OnEnable()
        {
            AddListener(PPREvents.player_object_awake, NotePlayerObjectID);
        }

        private void OnDisable()
        {
            AddListener(PPREvents.player_object_awake, DiscardPlayerObjectID);
        }

        private void Start()
        {
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