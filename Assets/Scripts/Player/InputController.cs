using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public class InputController : IUpdate, IBonusObserver
    {

        //public Vector3 force { get; private set; }
        private IInputData inputData;
        private Timer timerInverse;

        public InputController(IInputData inputData)
        {
            this.inputData = inputData;
            inputData.force = Vector3.zero;
            timerInverse = new Timer();
        }

        public void Update(float deltaTime)
        {
            timerInverse.Update(deltaTime);
            inputData.force = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            if (timerInverse.On) inputData.force *= -1f;
        }

        public void PlayerTakeBonus(IBonus bonus)
        {
            if (bonus.bonusType == BonusType.InputInverse)
            {
                timerInverse.Add(bonus.time);
            }
        }
    }
}
