using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class GameStarter : MonoBehaviour
    {

        private UpdateController updateController;

        private void Awake()
        {
            updateController = new UpdateController();
        }

        private void Start()
        {
            Time.timeScale = 1f;
            SetCursorVisible(false);
            Game game = new Game(updateController);
            game.Start();
        }

        private void Update()
        {
            updateController.Update(Time.deltaTime);
            updateController.UnscaledUpdate(Time.unscaledDeltaTime);
        }

        private void LateUpdate()
        {
            updateController.LateUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            updateController.FixedUpdate(Time.fixedDeltaTime);
        }

        public void SetCursorVisible(bool value)
        {
            if (value)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void OnDestroy()
        {
            SetCursorVisible(true);
        }
    }
}
