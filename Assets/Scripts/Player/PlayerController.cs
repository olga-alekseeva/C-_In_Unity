using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class PlayerController : IFixedUpdate, IBonusObserver, IEndGameEvent
    {

        public event Action actionEndGame;

        private IPlayer player;
        private GameObject gameObject;
        private Rigidbody rigidbody;
        private bool visible;
        private InputController inputController;
        private Timer doubleSpeedTimer;
        private Timer halfSpeedTimer;
        private MazeSettings mazeSettings;
        private IInputData inputData;

        public PlayerController(IPlayer player, IInputData inputData)
        {
            mazeSettings = new MazeSettings();
            this.player = player;
            this.player.speed = StartPlayerSpeed();
            this.inputData = inputData;
            gameObject = null;
            rigidbody = null;
            visible = false;
            doubleSpeedTimer = new Timer();
            halfSpeedTimer = new Timer();
            actionEndGame = delegate { };
        }

        public GameObject GetPlayer()
        {
            return gameObject;
        }

        private float StartPlayerSpeed()
        {
            return 5f;
        }

        private Vector3 StartPlayerPosition()
        {
            return new Vector3(mazeSettings.cellWidth / 2f, 0.5f, mazeSettings.cellHeight / 2f);
        }

        public void ShowPlayer()
        {
            if (!visible)
            {
                PlayerPrefabs playerPrefabs = new PlayerPrefabs();
                gameObject = GameObject.Instantiate(playerPrefabs.player, StartPlayerPosition(), Quaternion.identity);
                rigidbody = gameObject.GetComponent<Rigidbody>();
                if (rigidbody == null) throw new Exception("RigitBody expected on Player prefab");
                visible = true;
            }
        }

        public void FixedUpdate(float deltaTime)
        {
            doubleSpeedTimer.Update(deltaTime);
            halfSpeedTimer.Update(deltaTime);
            if (visible)
            {
                Vector3 force = inputData.force * player.speed;
                if (doubleSpeedTimer.On) force *= 2f;
                if (halfSpeedTimer.On) force /= 2f;
                rigidbody?.AddForce(force);
            }
        }

        public void PlayerTakeBonus(IBonus bonus)
        {
            if (bonus.bonusType == BonusType.Score)
            {
                player.score += bonus.score;
                if (player.score >= 1000) actionEndGame();
            }
            else if (bonus.bonusType == BonusType.DoubleSpeed)
            {
                doubleSpeedTimer.Add(bonus.time);
            }
            else if (bonus.bonusType == BonusType.HalfSpeed)
            {
                halfSpeedTimer.Add(bonus.time);
            }

        }
    }
}
