using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class Timer
    {
        private float time;
        private Action action;
        public bool On
        {
            get
            {
                return time > 0;
            }
        }

        public Timer()
        {
            time = 0;
            action = delegate { };
        }
        public Timer(float time)
        {
            this.time = time;
            action = delegate { };
        }

        public void Add(float time)
        {
            this.time += time;
        }

        public void Add(float time, Action action)
        {
            this.time += time;
            this.action = action;
        }
        public void Update(float deltaTime)
        {
            if (time > 0)
            {
                time -= deltaTime;
                if (time < 0) time = 0;
                if (time == 0)
                {
                    action();
                    action = delegate { };
                }
            }
        }
    }
}
