using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace MiniGames.DoorGame.Tiles
{
    public class FieldTileColor: MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Color enabledColor;
        [SerializeField] private Color disabledColor;

        private void Start()
        {
            image.rectTransform.sizeDelta = new Vector2(60, 60);
        }

        public void SetEnabledColor()
        {
            Debug.Log("set enabled");
            image.color = enabledColor;
        }

        public void SetDisabledColor()
        {
            image.color = disabledColor;
        }
    }
}