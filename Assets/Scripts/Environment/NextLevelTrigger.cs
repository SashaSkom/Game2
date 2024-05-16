using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Environment
{
    public class NextLevelTrigger: MonoBehaviour
    {
        [SerializeField] private Image screenPlaceholder;
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
            var animationStepTime = 3f / 100f;
            screenPlaceholder.gameObject.SetActive(true);
            for (var i = 0; i < 100; i++)
            {
                screenPlaceholder.color = new Color(screenPlaceholder.color.r, screenPlaceholder.color.g,
                    screenPlaceholder.color.b, screenPlaceholder.color.a + 0.01f);
                yield return new WaitForSeconds(animationStepTime);
            }
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}