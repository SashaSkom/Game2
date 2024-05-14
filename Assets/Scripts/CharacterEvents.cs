using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents: MonoBehaviour
{
    public UnityEvent enableMovements = new();
    public UnityEvent disableMovements = new();
}