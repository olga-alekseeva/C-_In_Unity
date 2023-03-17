using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class UseDelegate
    {
        public class BaseClass
        {
            public void BaseMethodIn(BaseClass baseClass)
            {

            }
            public BaseClass BaseMethodOut()
            {
                return new BaseClass();
            }
        }

        public sealed class ChildClass : BaseClass
        {
            public void ChildMethodIn(ChildClass baseClass)
            {

            }
            public ChildClass ChildMethodOut()
            {
                return new ChildClass();
            }
        }

        public void UseExample()
        {
            BaseClass baseClass = new BaseClass();
            ChildClass childClass = new ChildClass();

            CovariantDelegate<BaseClass> covariantDelegate = childClass.ChildMethodOut;

            ContrvariantDelegate<ChildClass> contrvariantDelegate = baseClass.BaseMethodIn;
        }
    }
}
