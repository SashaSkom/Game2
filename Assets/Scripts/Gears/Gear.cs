using UnityEngine;

namespace Gears
{
    public class Gear: MonoBehaviour
    {
        private GearsController gearsController;
        
        private void Start()
        {
            gearsController = FindObjectOfType<GearsController>().GetComponent<GearsController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Character>(out _))
            {
                gearsController.onGearCollected.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}