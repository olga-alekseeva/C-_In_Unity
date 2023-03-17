using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MenuScene
    {
        public void Start()
        {
            MainMenuController mainMenuController = new MainMenuController();
            mainMenuController.Show();
        }
    }
}
