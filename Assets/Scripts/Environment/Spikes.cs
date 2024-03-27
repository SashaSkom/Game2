using JetBrains.Annotations;
using UnityEngine;

namespace Environment
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] private GameObject respawnPoint;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (IsCharacter(col.gameObject, out var character))
            {
                Respawn(character);
            }
        }
        
        private void Respawn(Character character)
        {
            character.transform.position = respawnPoint.transform.position;
        }

        private bool IsCharacter(GameObject obj, [CanBeNull] out Character character)
        {
            character = obj.GetComponent<Character>();
            return character != null;
        }
    }
}
