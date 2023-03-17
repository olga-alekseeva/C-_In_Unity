using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class BonusObservers : IBonusObserver
    {
        private List<IBonusObserver> listObservers;

        public BonusObservers()
        {
            listObservers = new List<IBonusObserver>();
        }

        public void Add(IBonusObserver bonusObserver)
        {
            listObservers.Add(bonusObserver);
        }

        public void PlayerTakeBonus(IBonus bonus)
        {
            foreach(IBonusObserver bonusObserver in listObservers)
            {
                bonusObserver.PlayerTakeBonus(bonus);
            }
        }
    }
}
