using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu: MonoBehaviour
    {
        public string startScene = "SampleScene";
    
        public void StartGame()
        {
            SceneManager.LoadScene(startScene);
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}