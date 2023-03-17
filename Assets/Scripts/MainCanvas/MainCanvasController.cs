using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MainCanvasController
    {
        private GameObject gameObject;
        private bool visible;

        public GameObject Show()
        {
            if (visible)
            {
                return gameObject;
            }
            else
            {
                GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.mainCanvas);
                GameObject gameObject = GameObject.Instantiate(prefab);
                visible = true;
                return gameObject;
            }
        }

        public void Hide()
        {
            if (visible)
            {
                GameObject.Destroy(gameObject);
                gameObject = null;
                visible = false;
            }
        }
    }
}
