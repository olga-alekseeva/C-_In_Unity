using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MessageMatch
    {
        public IMessage message;
        public GameObject gameObject;
        public RectTransform rectTransform;
        public bool visible;
        public Timer timer;

        public MessageMatch(IMessage message, Timer timer)
        {
            this.message = message;
            this.gameObject = null;
            this.visible = false;
            this.timer = timer;
        }
    }
}
