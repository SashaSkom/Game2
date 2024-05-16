using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ToMainMenuButton: MonoBehaviour
    {
        public void ToMainMenu()
        {
            StaticStorage.GearsCount = 0;
            SceneManager.LoadScene("MainMenu");
        }
    }
}