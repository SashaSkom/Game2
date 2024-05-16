using MiniGames.DoorGame;
using UnityEngine;

namespace Environment
{
    public class StationDoor: MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject doorMiniGame;
        [SerializeField] private CharacterEvents characterEvents;
        [SerializeField] private bool canInteract = true;
        [SerializeField] private Transform insideStationPosition;
        [SerializeField] private Character character;
        [SerializeField] private CameraController cameraController;

        private void Start()
        {
            var gameEvents = FindObjectOfType<DoorGameEvents>().GetComponent<DoorGameEvents>();
            characterEvents = FindObjectOfType<CharacterEvents>().GetComponent<CharacterEvents>();
            character = FindObjectOfType<Character>().GetComponent<Character>();
            cameraController = FindObjectOfType<CameraController>().GetComponent<CameraController>();
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
            canInteract = false;
        }

        public void OnWinGameHandler()
        {
            EndMiniGame();
            characterEvents.enableMovements.Invoke();
            MovePlayerInsideStation();
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

        private void MovePlayerInsideStation()
        {
            character.transform.position = insideStationPosition.position;
            cameraController.transform.position = new Vector3(insideStationPosition.position.x,
                insideStationPosition.position.y, cameraController.transform.position.z);
        }
    }
}