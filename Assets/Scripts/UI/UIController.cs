using UnityEngine;

namespace UI
{
    public class UIController: MonoBehaviour
    {
        [SerializeField] private PauseMenu pauseMenu;

        public static UIController Instance;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            HandleInput();
        }

        public void OpenPauseMenu()
        {
            pauseMenu.gameObject.SetActive(true);
        }

        public void ClosePauseMenu()
        {
            pauseMenu.gameObject.SetActive(false);
        }
        
        
        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.gameObject.activeSelf)
                {
                    ClosePauseMenu();
                }
                else
                {
                    OpenPauseMenu();
                }
            }
        }
    }
}