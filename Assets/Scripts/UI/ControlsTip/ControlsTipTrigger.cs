using UnityEngine;

namespace UI.ControlsTip
{
    public class ControlsTipTrigger: MonoBehaviour
    {
        [SerializeField] private string tipText;
        [SerializeField] private ControlsTip controlsTip;
    
        private void Start()
        {
            controlsTip = FindObjectOfType<ControlsTip>().GetComponent<ControlsTip>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            controlsTip.ShowTipText(tipText);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            controlsTip.Hide();
        }
    }
}