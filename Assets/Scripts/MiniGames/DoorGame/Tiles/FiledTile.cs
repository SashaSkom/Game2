using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGames.DoorGame.Tiles
{
    public class FieldTile: MonoBehaviour, IPointerClickHandler
    {
        public Guid Id = Guid.NewGuid();
        
        [SerializeField] private FieldTileColor tileColor;
        private GameEvents _gameEvents;
        public bool IsOpened { get; private set; }

        private void Start()
        {
            _gameEvents = FindObjectOfType<GameEvents>().GetComponent<GameEvents>();
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
            _gameEvents.onTileClick.Invoke(Id);
            Debug.Log($"click {Id}");
        }
    }
}