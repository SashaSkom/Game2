using UnityEngine;
using UnityEngine.Events;

namespace Gears
{
    public class GearsController: MonoBehaviour
    {
        [SerializeField] public UnityEvent onGearCollected = new();
        [SerializeField] public UnityEvent<int> onSpendGears = new();
        [SerializeField] public UnityEvent notEnoughGears = new();
        [SerializeField] public UnityEvent onGearsCountChanged = new();

        private int gearsCount;
        public int GearsCount
        {
            get => gearsCount;
            private set
            {
                if (value == gearsCount) return;
                gearsCount = value;
                StaticStorage.GearsCount = value;
                Debug.Log(StaticStorage.GearsCount);
                onGearsCountChanged.Invoke();
            }
        }

        public bool TrySpendGears(int count)
        {
            if (count <= GearsCount)
            {
                GearsCount -= count;
                onSpendGears.Invoke(count);
                return true;
            } 
            notEnoughGears.Invoke();
            return false;
        }

        private void Start()
        {
            gearsCount = StaticStorage.GearsCount;
            onGearCollected.AddListener(OnGearCollectedHandler);
        }

        private void OnGearCollectedHandler()
        {
            GearsCount++;
        }
    }
}