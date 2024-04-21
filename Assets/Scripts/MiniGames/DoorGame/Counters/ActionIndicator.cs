using UnityEngine;
using UnityEngine.UI;

namespace MiniGames.DoorGame.Counters
{
    public class ActionIndicator: MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;
        
        public void On()
        {
            image.color = onColor;
        }

        public void Off()
        {
            image.color = offColor;
        }
    }
}