using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oliasca.Maze
{
    public sealed class PauseMenuController : IUpdate
    {
        private GameObject prefabMenu;
        private GameObject pauseMenu;
        private PauseMenuBehaviour pauseMenuBehaviour;
        private bool visible;

        public PauseMenuController()
        {
            prefabMenu = Resources.Load<GameObject>(ResourcesPathes.pauseMenu);
            visible = false;
        }

        public void Show()
        {
            if (!visible)
            {
                pauseMenu = GameObject.Instantiate(prefabMenu);
                visible = true;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;

                pauseMenuBehaviour = pauseMenu.GetComponentInChildren<PauseMenuBehaviour>();
                if (pauseMenuBehaviour == null) throw new System.Exception("Pause menu have no PauseMenuBehaviour");

                pauseMenuBehaviour.buttonContinue.onClick.AddListener(ButtonContinuePressed);
                pauseMenuBehaviour.buttonExit.onClick.AddListener(ButtonExitPressed);
            }
        }

        public void Hide()
        {
            if (visible)
            {
                GameObject.Destroy(pauseMenu);
                pauseMenu = null;
                visible = false;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) EscapePressed();
        }

        private void EscapePressed()
        {
            if (visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void ButtonContinuePressed()
        {
            Hide();
        }

        private void ButtonExitPressed()
        {
            SceneManager.LoadScene(0);
        }
    }
}
