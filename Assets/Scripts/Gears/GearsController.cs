using UnityEngine;
using UnityEngine.Events;

namespace Gears
{
    public class GearsController: MonoBehaviour
    {
        [SerializeField] public UnityEvent onGearCollected = new();
        [SerializeField] public UnityEvent<int> onTrySpendGears = new();
        [SerializeField] public UnityEvent notEnoughGears = new();

        public int GearsCount { get; private set; }

        private void Start()
        {
            onGearCollected.AddListener(OnGearCollectedHandler);
            onTrySpendGears.AddListener(OnTrySpendGearsHandler);
        }

        private void OnGearCollectedHandler()
        {
            GearsCount++;
        }
        
        private void OnTrySpendGearsHandler(int count)
        {
            if (count <= GearsCount)
            {
                GearsCount -= count;
            }
            else
            {
                notEnoughGears.Invoke();
            }
        }
    }
}