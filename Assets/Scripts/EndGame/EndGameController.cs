using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Oliasca.Maze
{
    public sealed class EndGameController : IDisposable
    {
        private List<IEndGameEvent> sourceList;
        public EndGameController()
        {
            sourceList = new List<IEndGameEvent>();
        }

        public void AddEventSource(IEndGameEvent endGameEventSource)
        {
            endGameEventSource.actionEndGame += EndGame;
            sourceList.Add(endGameEventSource);
        }
        public void RemoveEventSource(IEndGameEvent endGameEventSource)
        {
            endGameEventSource.actionEndGame -= EndGame;
            sourceList.Remove(endGameEventSource);
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
        public void EndGame()
        {
            SetCursorVisible(true);
            Time.timeScale = 0f;
            EndGamePrefab endGamePrefab = new EndGamePrefab();
            GameObject gameObject = GameObject.Instantiate(endGamePrefab.endGame, Vector3.zero, Quaternion.identity);
            Button buttonRestart = gameObject.GetComponentInChildren<Button>();
            if (buttonRestart == null) 
            {
                throw new Exception("Button Restart expected on EndGamePrefab");
            }
            else
            {
                buttonRestart.onClick.AddListener(Restart);
            }
            
        }

        public void Restart()
        {
            SceneManager.LoadScene(1);
        }

        public void Dispose()
        {
            foreach(IEndGameEvent endGameEventSource in sourceList)
            {
                if (endGameEventSource != null) endGameEventSource.actionEndGame -= EndGame;
            }
            sourceList.Clear();
        }

    }
}
