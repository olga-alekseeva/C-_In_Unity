using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public class MenuSceneStarterBehaviour : MonoBehaviour
    {
        void Start()
        {
            MenuScene menuScene = new MenuScene();
            menuScene.Start();
        }
    }
}
