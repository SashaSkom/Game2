using System.Collections;
using MiniGames.Light;
using UnityEngine;

namespace Environment
{
    public class LightTerminal: MonoBehaviour, IInteractable
    {
        [SerializeField] private bool canInteract = true;
        [SerializeField] private Transform gamePosition;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private CharacterEvents characterEvents;
        [SerializeField] private GameObject lightGameUI;
        [SerializeField] private LightGameEvents lightGameEvents;
        [SerializeField] private Character character;

        private void Start()
        {
            cameraController = FindObjectOfType<CameraController>().GetComponent<CameraController>();
            characterEvents = FindObjectOfType<CharacterEvents>().GetComponent<CharacterEvents>();
            lightGameEvents = FindObjectOfType<LightGameEvents>().GetComponent<LightGameEvents>();
            character = FindObjectOfType<Character>().GetComponent<Character>();
            lightGameEvents.onWinGame.AddListener(OnWinGameHandler);
        }

        public bool CanInteract()
        {
            return canInteract;
        }

        public void Interact()
        {
            if(!canInteract) return;
            canInteract = false;
            characterEvents.disableMovements.Invoke();
            cameraController.followPlayer = false;
            cameraController.transform.position = new Vector3(gamePosition.position.x, gamePosition.position.y, cameraController.transform.position.z);
            lightGameUI.SetActive(true);
        }

        private void OnWinGameHandler()
        {
            StartCoroutine(FinishGameCoroutine());
        }

        private IEnumerator FinishGameCoroutine()
        {
            yield return new WaitForSeconds(1);
            cameraController.transform.position = character.transform.position;
            cameraController.followPlayer = true;
            characterEvents.enableMovements.Invoke();
            lightGameUI.SetActive(false);
        }
    }
}