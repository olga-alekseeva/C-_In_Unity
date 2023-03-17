using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oliasca.Maze
{
    public sealed class MainMenuController
    {
        private GameObject canvasRoot;
        private GameObject mainMenu;
        private MainMenuBehaviour mainMenuBehaviour;
        private ProfileListController profileListController;
        private NewProfileController newProfileController;
        private SelectProfileController selectProfileController;

        public MainMenuController()
        {
            profileListController = new ProfileListController();
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

        private void UpdateVisibleElements()
        {
            if (profileListController.profileList.lastProfile == null)
            {
                mainMenuBehaviour.textProfileName.text = "<Профиль не выбран>";
                mainMenuBehaviour.buttonStart.interactable = false;
            }
            else
            {
                mainMenuBehaviour.textProfileName.text = "Профиль: " + profileListController.profileList.lastProfile.name;
                mainMenuBehaviour.buttonStart.interactable = true;
            }
        }

        public void Show()
        {
            InitCanvasRoot();
            GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.mainMenu);
            mainMenu = GameObject.Instantiate(prefab);
            mainMenu.transform.SetParent(canvasRoot.transform);
            mainMenu.transform.localPosition = Vector3.zero;
            mainMenuBehaviour = mainMenu.GetComponent<MainMenuBehaviour>();
            if (mainMenuBehaviour == null) throw new System.Exception("Prefab MeniMenu have no MainMenuBehaviour");
            UpdateVisibleElements();
            mainMenuBehaviour.buttonNewProfile.onClick.AddListener(NewProfileButtonPressed);
            mainMenuBehaviour.buttonSelectProfile.onClick.AddListener(SelectProfileButtonPressed);
            mainMenuBehaviour.buttonExit.onClick.AddListener(ButtonExitPressed);
            mainMenuBehaviour.buttonStart.onClick.AddListener(ButtonStartPressed);
        }

        private void ButtonStartPressed()
        {
            GameObject gameObject = new GameObject("SelectedPlayerProfile");
            SelectedPlayerProfileBehaviour selectedPlayerProfileBehaviour = gameObject.AddComponent<SelectedPlayerProfileBehaviour>();
            selectedPlayerProfileBehaviour.playerProfile = profileListController.profileList.lastProfile;
            GameObject.DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(1);
        }

        private void ButtonExitPressed()
        {
            Application.Quit();
        }

        private void SelectProfileButtonPressed()
        {
            selectProfileController = new SelectProfileController(profileListController.profileList);
            selectProfileController.Show();
            selectProfileController.actionButtonCancelPressed += SelectProfileButtonCancelPressed;
            selectProfileController.actionPlayerProfileButtonPressed += SelectProfileButtonPressed;
        }

        private void SelectProfileButtonPressed(PlayerProfile playerProfile)
        {
            profileListController.SetLastProfile(playerProfile);
            selectProfileController.Hide();
            UpdateVisibleElements();
        }

        private void SelectProfileButtonCancelPressed()
        {
            selectProfileController.Hide();
        }

        private void NewProfileButtonPressed()
        {
            newProfileController = new NewProfileController();
            newProfileController.Show();
            newProfileController.actionButtonOkPressed += NewProfileButtonOkPressed;
            newProfileController.actionButtonCancelPressed += NewProfileButtonCancelPressed;
        }

        private void NewProfileButtonOkPressed(string name)
        {
            profileListController.AddNewProfile(name);
            newProfileController.Hide();
            UpdateVisibleElements();
        }

        private void NewProfileButtonCancelPressed()
        {
            newProfileController.Hide();
        }
    }
}
