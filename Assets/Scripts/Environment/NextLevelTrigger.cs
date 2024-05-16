using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class NextLevelTrigger: MonoBehaviour
    {
        [SerializeField] private Sprite screenPlaceholder;
        [SerializeField] private string nextLevelSceneName;
        private bool triggered;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(triggered) return;
            if (col.TryGetComponent<Character>(out _))
            {
                triggered = true;
                StartCoroutine(ToNextLevelCoroutine());
            }
        }

        private IEnumerator ToNextLevelCoroutine()
        {
            //TODO затемнение экрана
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}