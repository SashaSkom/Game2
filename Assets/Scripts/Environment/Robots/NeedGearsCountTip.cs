using TMPro;
using UnityEngine;

namespace Environment.Robots
{
    public class NeedGearsCountTip: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Robot robot;

        private void Start()
        {
            countText.text = $"{robot.NeedGearsToRepair}";
        }

        private void Update()
        {
            if (!robot.isBroken)
            {
                gameObject.SetActive(false);
            }
        }
    }
}