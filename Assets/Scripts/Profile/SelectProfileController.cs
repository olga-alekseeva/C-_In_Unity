using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class SelectProfileController
    {
        public event Action actionButtonCancelPressed;
        public event Action<PlayerProfile> actionPlayerProfileButtonPressed;

        private GameObject canvasRoot;
        private GameObject selectProfile;
        private SelectProfileBehaviour selectProfileBehaviour;

        private ProfileList profileList;

        public SelectProfileController(ProfileList profileList)
        {
            this.profileList = profileList;
        }

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
            GameObject.Destroy(selectProfile);
            selectProfile = null;
            selectProfileBehaviour = null;
        }

        public void Show()
        {
            InitCanvasRoot();
            GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.selectProfile);
            selectProfile = GameObject.Instantiate(prefab);
            selectProfile.transform.SetParent(canvasRoot.transform);
            selectProfile.transform.localPosition = Vector3.zero;
            RectTransform rectTransform = selectProfile.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            selectProfileBehaviour = selectProfile.GetComponent<SelectProfileBehaviour>();
            if (selectProfileBehaviour == null)
            {
                throw new System.Exception("Prefab SelectProfile have no SelectProfileBehaviour");
            }
            selectProfileBehaviour.buttonCancel.onClick.AddListener(ButtonCancelPressed);
            InstantiateProfileButtons();
        }

        private void InstantiateProfileButtons()
        {
            GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.selectProfileButton);

            RectTransform contentRect = selectProfileBehaviour.contentRoot.GetComponent<RectTransform>();

            float yPosition = 0f;

            if (profileList.list.Count == 0)
            {
                contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
                yPosition = 0f;
            }
            else
            {
                yPosition = (float)profileList.list.Count * 35f - 5f;
                contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, yPosition);
            }

            foreach(PlayerProfile playerProfile in profileList.list)
            {
                GameObject profileButtonGameObject = GameObject.Instantiate(prefab);
                profileButtonGameObject.transform.SetParent(selectProfileBehaviour.contentRoot.transform);
                profileButtonGameObject.transform.localPosition = new Vector3(0f, -yPosition + 15f, 0f);
                yPosition = yPosition - 35f;

                ButtonProfileBehaviour buttonProfileBehaviour = profileButtonGameObject.GetComponent<ButtonProfileBehaviour>();
                buttonProfileBehaviour.SetProfile(playerProfile);
                buttonProfileBehaviour.actionSelectProfileButtonPressed += SelectProfileButtonPressed;

            }
        }

        private void SelectProfileButtonPressed(PlayerProfile playerProfile)
        {
            actionPlayerProfileButtonPressed?.Invoke(playerProfile);
        }

        private void ButtonCancelPressed()
        {
            actionButtonCancelPressed?.Invoke();
        }

    }
}
