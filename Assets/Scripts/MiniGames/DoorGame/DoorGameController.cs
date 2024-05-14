using System;
using MiniGames.DoorGame.Counters;
using MiniGames.DoorGame.Tiles;
using UnityEngine;

namespace MiniGames.DoorGame
{
    [Serializable]
    public class DoorGameController: MonoBehaviour
    {
        [Min(0)] [SerializeField] private int showPatternTimeInSeconds = 5;
        [Range(1, 10)] [SerializeField] private int passwordLength = 4;
        [Range(1, 10)] [SerializeField] private int fieldSize = 5;
        [SerializeField] private int allowedMistakesCount = 3;
        [SerializeField] private int patternSize = 7;
        
        [SerializeField] private int correctAnswersCount;
        [SerializeField] private int mistakesCount;
        
        [SerializeField] private FieldController fieldController;
        [SerializeField] private ActionsCounter mistakesCounter;
        [SerializeField] private ActionsCounter correctPatternsCounter;
        
        private DoorGameEvents _doorGameEvents;

        private void Start()
        {
            fieldController.Init(fieldSize);
            mistakesCounter.Init(allowedMistakesCount);
            correctPatternsCounter.Init(passwordLength);
            
            _doorGameEvents = FindObjectOfType<DoorGameEvents>().GetComponent<DoorGameEvents>();
            _doorGameEvents.onWinGame.AddListener(OnWinGameHandler);
            _doorGameEvents.onLooseGame.AddListener(OnLooseGameHandler);
            _doorGameEvents.onLooseRound.AddListener(OnLooseRoundHandler);
            _doorGameEvents.onWinRound.AddListener(OnWinRoundHandler);
            
        }
        public void StartGame()
        {
            correctAnswersCount = 0;
            mistakesCount = 0;
            fieldController.StartRound(Math.Min(patternSize, fieldSize*fieldSize), showPatternTimeInSeconds);
        }

        private void OnWinGameHandler()
        {
            Debug.Log("Win");
        }

        private void OnLooseGameHandler()
        {
            Debug.Log("Loose");
        }

        private void OnWinRoundHandler()
        {
            if (correctAnswersCount == passwordLength-1)
            {
                _doorGameEvents.onWinGame.Invoke();
            }
            else
            {
                correctAnswersCount++;
                correctPatternsCounter.Increase();
                fieldController.StartRound(patternSize, showPatternTimeInSeconds);
            }
        }

        private void OnLooseRoundHandler()
        {
            if (mistakesCount == allowedMistakesCount)
            {
                _doorGameEvents.onLooseGame.Invoke();
            }
            else
            {
                mistakesCount++;
                mistakesCounter.Increase();
                fieldController.StartRound(patternSize, showPatternTimeInSeconds);
            }
        }
    }
}