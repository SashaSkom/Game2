using System.Collections.Generic;
using UnityEngine;

namespace Gears
{
    public class OnDestroyGearsSpawner: MonoBehaviour
    {
        [SerializeField] private GameObject gearPrefab;
        [SerializeField] private List<Transform> spawnPoints;

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            foreach (var spawnPoint in spawnPoints)
            {
                Debug.Log("inst");
                Instantiate(gearPrefab, spawnPoint);
            }
        }
    }
}