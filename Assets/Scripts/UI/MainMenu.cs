using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu: MonoBehaviour
    {
    
        public void StartGame()
        {
            SceneManager.LoadScene(Constants.SceneNames.Level1);
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}