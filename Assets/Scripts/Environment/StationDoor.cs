using MiniGames.DoorGame;
using UnityEngine;

namespace Environment
{
    public class StationDoor: MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject doorMiniGame;
        [SerializeField] private CharacterEvents characterEvents;
        [SerializeField] private bool canInteract = true;

        private void Start()
        {
            var gameEvents = FindObjectOfType<DoorGameEvents>().GetComponent<DoorGameEvents>();
            characterEvents = FindObjectOfType<CharacterEvents>().GetComponent<CharacterEvents>();
            gameEvents.onWinGame.AddListener(OnWinGameHandler);
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
            canInteract = true;
        }

        public void OnWinGameHandler()
        {
            EndMiniGame();
            gameObject.SetActive(false);
        }

        public bool CanInteract()
        {
            return canInteract;
        }

        public void Interact()
        {
            if(doorMiniGame.activeSelf || !canInteract) return;
            canInteract = false;
            StartMiniGame();
        }
    }
}