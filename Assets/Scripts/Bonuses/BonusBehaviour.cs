using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public class BonusBehaviour : MonoBehaviour
    {
        public event Action<(BonusMatch, Collider)> onTriggerEnter = delegate ((BonusMatch bonusMatch, Collider collider) tuple) { };

        public BonusMatch bonusMatch;

        private void OnTriggerEnter(Collider other)
        {
            (BonusMatch bonusMatch, Collider collider) tuple = (bonusMatch, other);
            onTriggerEnter(tuple);
        }
    }
}
