using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGames.DoorGame.Tiles
{
    public class FieldTile: MonoBehaviour, IPointerClickHandler
    {
        public Guid Id = Guid.NewGuid();
        
        [SerializeField] private FieldTileColor tileColor;
        private DoorGameEvents _doorGameEvents;
        public bool IsOpened { get; private set; }

        private void Start()
        {
            _doorGameEvents = FindObjectOfType<DoorGameEvents>().GetComponent<DoorGameEvents>();
            tileColor = GetComponent<FieldTileColor>();
        }

        public void Open()
        {
            IsOpened = true;
            tileColor.SetEnabledColor();
        }

        public void Hide()
        {
            IsOpened = false;
            tileColor.SetDisabledColor();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(IsOpened) return;
            _doorGameEvents.onTileClick.Invoke(Id);
            Debug.Log($"click {Id}");
        }
    }
}