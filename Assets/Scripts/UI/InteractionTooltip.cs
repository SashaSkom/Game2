using TMPro;
using UnityEngine;

namespace UI
{
    public class InteractionTooltip: MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private TextMeshProUGUI tooltipText;

        private void Start()
        {
            character = FindObjectOfType<Character>().GetComponent<Character>();
        }

        private void Update()
        {
            tooltipText.gameObject.SetActive(character.CanInteract);
        }
    }
}