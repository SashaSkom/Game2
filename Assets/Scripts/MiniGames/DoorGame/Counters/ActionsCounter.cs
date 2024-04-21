using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.DoorGame.Counters
{
    public abstract class ActionsCounter: MonoBehaviour
    {
        [SerializeField] private ActionIndicator actionActionIndicatorPrefab;
        private readonly List<ActionIndicator> _actionIndicators = new();
        private int _actionsCount;
        private GameEvents _gameEvents;
        
        protected void Start()
        {
            _gameEvents = FindObjectOfType<GameEvents>().GetComponent<GameEvents>();
            _gameEvents.onLooseGame.AddListener(Reset);
            _gameEvents.onWinGame.AddListener(Reset);
        }

        public void Init(int maxCount)
        {
            for (var i = 0; i < maxCount; i++)
            {
                var indicator = Instantiate(actionActionIndicatorPrefab, transform);
                indicator.Off();
                _actionIndicators.Add(indicator);
            }
        }

        public void Increase()
        {
            if(_actionsCount == _actionIndicators.Count) return;
            _actionIndicators[_actionsCount].On();
            _actionsCount++;
        }

        public void Reset()
        {
            foreach (var indicator in _actionIndicators)
            {
                indicator.Off();
            }

            _actionsCount = 0;
        }
    }
}