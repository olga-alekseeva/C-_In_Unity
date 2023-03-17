using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class Message : IMessage
    {
        public string text { get; set; }

        public Message(string text)
        {
            this.text = text;
        }
    }
}
