using TMPro;
using UnityEngine;

namespace UI.ControlsTip
{
    public class ControlsTip: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tipText;

        public void ShowTipText(string text)
        {
            tipText.gameObject.SetActive(true);
            tipText.text = text;
        }

        public void Hide()
        {
            tipText.gameObject.SetActive(false);
        }
    }
}