using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class NewProfileController
    {

        public event Action<string> actionButtonOkPressed;
        public event Action actionButtonCancelPressed;

        private GameObject canvasRoot;
        private GameObject newProfile;
        private NewProfileBehaviour newProfileBehaviour;

        private void InitCanvasRoot()
        {
            Canvas canvasComponent = GameObject.FindObjectOfType<Canvas>();
            if (canvasComponent == null)
            {
                canvasRoot = new MainCanvasController().Show();
            }
            else
            {
                canvasRoot = canvasComponent.gameObject;
            }
        }

        public void Hide()
        {
            GameObject.Destroy(newProfile);
            newProfile = null;
            newProfileBehaviour = null;
        }

        public void Show()
        {
            InitCanvasRoot();
            GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.newProfile);
            newProfile = GameObject.Instantiate(prefab);
            newProfile.transform.SetParent(canvasRoot.transform);
            newProfile.transform.localPosition = Vector3.zero;
            RectTransform rectTransform = newProfile.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            newProfileBehaviour = newProfile.GetComponent<NewProfileBehaviour>();
            if (newProfileBehaviour == null)
            { 
                throw new System.Exception("Prefab MeniMenu have no NewProfileBehaviour");
            }
            newProfileBehaviour.buttonOk.onClick.AddListener(ButtonOkPressed);
            newProfileBehaviour.buttonCancel.onClick.AddListener(ButtonCancelPressed);
            newProfileBehaviour.inputField.onEndEdit.AddListener(SubmitEvent);

            newProfileBehaviour.inputField.ActivateInputField();
        }

        private void SubmitEvent(string value)
        {
            actionButtonOkPressed?.Invoke(value);
        }

        private void ButtonOkPressed()
        {
            actionButtonOkPressed?.Invoke(newProfileBehaviour.inputField.text);
        }

        private void ButtonCancelPressed()
        {
            actionButtonCancelPressed?.Invoke();
        }
    }
}
