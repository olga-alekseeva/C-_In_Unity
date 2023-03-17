using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MinimapController : ILateUpdate
    {
        private GameObject player;
        private GameObject minimapCamera;
        private GameObject minimapCanvas;
        
        public MinimapController(GameObject player)
        {
            this.player = player;
            Show();
        }

        public void Show()
        {
            GameObject minimapCameraPrefab = Resources.Load<GameObject>(ResourcesPathes.minimapCamera);
            minimapCamera = GameObject.Instantiate(minimapCameraPrefab);
            GameObject minimapCanvasPrefab = Resources.Load<GameObject>(ResourcesPathes.minimapCanvas);
            minimapCanvas = GameObject.Instantiate(minimapCanvasPrefab);
        }

        public void Hide()
        {
            GameObject.Destroy(minimapCamera);
            GameObject.Destroy(minimapCanvas);
        }

        public void LateUpdate(float deltaTime)
        {
            minimapCamera.transform.position = player.transform.position + Vector3.up * 10f;
        }
    }
}
