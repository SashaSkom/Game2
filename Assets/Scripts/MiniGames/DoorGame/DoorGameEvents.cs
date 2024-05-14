using System;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGames.DoorGame
{
    public class DoorGameEvents: MonoBehaviour
    {
        public UnityEvent<Guid> onTileClick = new();
        public UnityEvent onLooseRound = new();
        public UnityEvent onLooseGame = new();
        public UnityEvent onWinRound = new();
        public UnityEvent onWinGame = new();
    }
}