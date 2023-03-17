using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class UpdateController
    {
        private List<IUpdate> listUpdate;
        private List<ILateUpdate> listLateUpdate;
        private List<IFixedUpdate> listFixedUpdate;
        private List<IUnscaledUpdate> listUnscaledUpdate;

        public UpdateController()
        {
            listUpdate = new List<IUpdate>();
            listLateUpdate = new List<ILateUpdate>();
            listFixedUpdate = new List<IFixedUpdate>();
            listUnscaledUpdate = new List<IUnscaledUpdate>();
        }

        public void Add(IUpdatable updatable)
        {
            if (updatable is IUpdate update) listUpdate.Add(update);
            if (updatable is ILateUpdate lateUpdate) listLateUpdate.Add(lateUpdate);
            if (updatable is IFixedUpdate fixedUpdate) listFixedUpdate.Add(fixedUpdate);
            if (updatable is IUnscaledUpdate unscaledUpdate) listUnscaledUpdate.Add(unscaledUpdate);
        }

        public void Update(float deltaTime)
        {
            foreach (IUpdate update in listUpdate) update.Update(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            foreach (ILateUpdate lateUpdate in listLateUpdate) lateUpdate.LateUpdate(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (IFixedUpdate fixedUpdate in listFixedUpdate) fixedUpdate.FixedUpdate(fixedDeltaTime);
        }
        public void UnscaledUpdate(float unscaledDeltaTime)
        {
            foreach (IUnscaledUpdate unscaledUpdate in listUnscaledUpdate) unscaledUpdate.UnscaledUpdate(unscaledDeltaTime);
        }
    }
}
