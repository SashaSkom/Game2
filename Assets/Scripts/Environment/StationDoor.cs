using MiniGames.DoorGame;
using UnityEngine;

namespace Environment
{
    public class StationDoor: MonoBehaviour
    {
        [SerializeField] private GameObject doorMiniGame;
        [SerializeField] private CharacterEvents characterEvents;

        private void Start()
        {
            var gameEvents = FindObjectOfType<DoorGameEvents>().GetComponent<DoorGameEvents>();
            characterEvents = FindObjectOfType<CharacterEvents>().GetComponent<CharacterEvents>();
            gameEvents.onWinGame.AddListener(OnWinGameHandler);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            StartMiniGame();
        }

        public void StartMiniGame()
        {
            doorMiniGame.SetActive(true);
            characterEvents.disableMovements.Invoke();
        }

        public void EndMiniGame()
        {
            doorMiniGame.SetActive(false);
            characterEvents.enableMovements.Invoke();
        }

        public void OnWinGameHandler()
        {
            EndMiniGame();
            gameObject.SetActive(false);
        }
    }
}