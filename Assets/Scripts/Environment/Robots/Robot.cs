using System.Collections;
using Gears;
using UnityEngine;

namespace Environment.Robots
{
    public class Robot: MonoBehaviour, IInteractable
    {
        public int NeedGearsToRepair { get; private set; } = 5;
        public bool isBroken = true;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color repairedColor = Color.green;
        [SerializeField] private float speed = 4f;
        [SerializeField] private RobotsController robotsController;

        [SerializeField] private GearsController gearsController;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            gearsController = FindObjectOfType<GearsController>().GetComponent<GearsController>();
            robotsController = FindObjectOfType<RobotsController>().GetComponent<RobotsController>();
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
            //TODO change sprite
            Debug.Log("Repair");
            sprite.color = repairedColor;
            robotsController.OnRobotRepaired.Invoke();
            StartCoroutine(MovementsCoroutine());
        }

        private IEnumerator MovementsCoroutine()
        {
            while (true)
            {
                _rigidbody2D.velocity = new Vector2(speed, 0);
                yield return new WaitForSeconds(3);
                _rigidbody2D.velocity = new Vector2(-speed, 0);
                yield return new WaitForSeconds(3);
            }
        }
    }
}