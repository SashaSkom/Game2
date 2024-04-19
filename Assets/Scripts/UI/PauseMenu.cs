using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public void ToMainMenu()
        {
            SceneManager.LoadScene(Constants.SceneNames.MainMenu);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Close()
        {
            UIController.Instance.ClosePauseMenu();
        }
    }
}
