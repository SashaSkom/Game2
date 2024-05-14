using Gears;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GearsCounter: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        private GearsController gearsController;
        private const string countPlaceholder = "{count}";
        private readonly string countTextTemplate = $"Количество шестеренок: {countPlaceholder}";
        
        private void Start()
        {
            gearsController = FindObjectOfType<GearsController>().GetComponent<GearsController>();
            gearsController.onGearsCountChanged.AddListener(OnGearsCountChangedHandler);
        }

        private void OnGearsCountChangedHandler()
        {
            countText.text = countTextTemplate.Replace(countPlaceholder, gearsController.GearsCount.ToString());
        }
    }
}