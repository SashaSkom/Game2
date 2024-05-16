using UnityEngine;

namespace Environment.FallingTree
{
    public class FallingTreeController: MonoBehaviour, IInteractable
    {
        [SerializeField] private bool canInteract = true;

        public bool CanInteract()
        {
            return canInteract;
        }

        public void Interact()
        {
            canInteract = false;
            transform.Rotate(0, 0, -10);
        }
    }
}