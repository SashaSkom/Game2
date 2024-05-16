using UnityEngine;
using UnityEngine.Events;

namespace MiniGames.Light
{
    public class LightGameEvents: MonoBehaviour
    {
        public UnityEvent onWinGame = new();
    }
}