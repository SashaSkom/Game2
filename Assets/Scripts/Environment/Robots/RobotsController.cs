using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Environment.Robots
{
    public class RobotsController: MonoBehaviour
    {
        public UnityEvent OnRobotRepaired = new();

        [SerializeField] private int needToRepair = 3;
        [SerializeField] private int repaired;

        private void Start()
        {
            OnRobotRepaired.AddListener(OnRobotRepairedHandler);
        }

        private void OnRobotRepairedHandler()
        {
            repaired++;
            if (repaired == needToRepair)
            {
                SceneManager.LoadScene("End");
            }
        }
    }
}