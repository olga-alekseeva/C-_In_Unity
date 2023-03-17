using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class Game
    {
        private UpdateController updateController;
        private InputData inputData;
        private InputController inputController;
        private Player player;
        private PlayerController playerController;
        private CameraController cameraController;
        private BonusController bonusController;
        private MessageInformerController messageInformerController;
        private MinimapController minimapController;
        private RadarController radarController;
        private PhotoController photoController;
        private ProfileController profileController;
        private PauseMenuController pauseMenuController;

        public Game(UpdateController updateController)
        {
            this.updateController = updateController;
        }

        public void Start()
        {
            InitMaze();
            InitInputData();
            InitInputController();
            InitPlayer();
            InitProfileController();
            InitPlayerController();
            InitCameraController();
            InitBonusController();
            InitEndGameController();
            InitMessageInformerController();
            InitMinimapController();
            InitRadarController();
            LoadBonuses();
            InitPhotoController();
            InitPauseMenuController();
            UpdateControllerRelation();
        }

        private void InitPauseMenuController()
        {
            pauseMenuController = new PauseMenuController();
        }

        private void InitProfileController()
        {
            SelectedPlayerProfileBehaviour selectedPlayerProfileBehaviour = GameObject.FindObjectOfType<SelectedPlayerProfileBehaviour>();
            if (selectedPlayerProfileBehaviour == null) throw new Exception("Player profile not loaded");
            profileController = new ProfileController(selectedPlayerProfileBehaviour.playerProfile, player);
            profileController.Load();
            GameObject.Destroy(selectedPlayerProfileBehaviour.gameObject);
        }

        private void InitPhotoController()
        {
            photoController = new PhotoController();
        }

        private void LoadBonuses()
        {
            bonusController.LoadBonuses();
        }

        private void InitRadarController()
        {
            radarController = new RadarController(playerController.GetPlayer());
            radarController.AddRadarObjectSource(bonusController);
        }

        private void InitMinimapController()
        {
            minimapController = new MinimapController(playerController.GetPlayer());
        }

        private void InitInputData()
        {
            inputData = new InputData();
        }

        private void InitMaze()
        {
            MazeController mazeController = new MazeController(new Maze());
            mazeController.Generate();
            mazeController.ShowMaze();
        }

        private void UpdateControllerRelation()
        {
            updateController.Add(inputController);
            updateController.Add(playerController);
            updateController.Add(cameraController);
            updateController.Add(bonusController);
            updateController.Add(messageInformerController);
            updateController.Add(minimapController);
            updateController.Add(radarController);
            updateController.Add(photoController);
            updateController.Add(pauseMenuController);
        }

        private void InitInputController()
        {
            inputController = new InputController(inputData);
        }

        private void InitPlayer()
        {
            player = new Player();
        }

        private void InitPlayerController()
        {
            playerController = new PlayerController(player, inputData);
            playerController.ShowPlayer();
        }

        private void InitCameraController()
        {
            cameraController = new CameraController(new CameraData(), playerController.GetPlayer());
            cameraController.ShowCamera();
        }

        private void InitBonusController()
        {
            BonusObservers bonusObservers = new BonusObservers();
            bonusObservers.Add(inputController);
            bonusObservers.Add(playerController);
            bonusObservers.Add(cameraController);
            bonusObservers.Add(profileController);

            bonusController = new BonusController(bonusObservers);
        }

        private void InitEndGameController()
        {
            EndGameController endGameController = new EndGameController();
            endGameController.AddEventSource(playerController);
        }

        private void InitMessageInformerController()
        {
            messageInformerController = new MessageInformerController();
            messageInformerController.AddMessageSource(bonusController);
        }
    }
}
