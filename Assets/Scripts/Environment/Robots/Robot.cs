using Gears;
using UnityEngine;

namespace Environment.Robots
{
    public class Robot: MonoBehaviour, IInteractable
    {
        public int NeedGearsToRepair { get; private set; } = 4;
        public bool isBroken = true;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color repairedColor = Color.green;

        [SerializeField] private GearsController gearsController;

        private void Start()
        {
            gearsController = FindObjectOfType<GearsController>().GetComponent<GearsController>();
            sprite = GetComponent<SpriteRenderer>();
        }

        public bool CanInteract()
        {
            return isBroken;
        }

        public void Interact()
        {
            if (gearsController.TrySpendGears(NeedGearsToRepair))
            {
                isBroken = false;
                Repair();
            }
        }

        private void Repair()
        {
            //TODO
            Debug.Log("Repair");
            sprite.color = repairedColor;
        }
    }
}