using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class CameraController : ILateUpdate, IBonusObserver
    {
        private GameObject gameObject;
        private bool visible;
        private GameObject playerGameObject;
        private ICameraData cameraData;
        private Timer cameraFieldTimer;

        public CameraController(ICameraData cameraData, GameObject player)
        {
            this.cameraData = cameraData;
            playerGameObject = player;
            gameObject = null;
            visible = false;
            cameraFieldTimer = new Timer();
        }

        private Vector3 StartPosition()
        {
            return new Vector3(3f, 10.5f, 3f);
        }

        private Quaternion StartRotation()
        {
            return Quaternion.Euler(90f, 0f, 0f);
        }

        public void ShowCamera()
        {
            if (!visible)
            {
                CameraPrefab cameraPrefab = new CameraPrefab();
                gameObject = GameObject.Instantiate(cameraPrefab.cameraGameObject, StartPosition(), StartRotation());
                visible = true;
            }
        }

        public void LateUpdate(float deltaTime)
        {
            cameraFieldTimer.Update(deltaTime);
            Vector3 targetPosition = playerGameObject.transform.position + Vector3.up * 20f;
            if (cameraFieldTimer.On)
            {
                targetPosition += Vector3.up * 30f;
            }

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, deltaTime * cameraData.speed);
        }

        public void PlayerTakeBonus(IBonus bonus)
        {
            if (bonus.bonusType == BonusType.CameraField)
            {
                cameraFieldTimer.Add(bonus.time);
            }

        }
    }
}
