using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Oliasca.Maze
{
    public sealed class PhotoController : IUpdate
    {
        private bool isProcessed;
        private readonly string path;
        private int resolution = 5;
        private const string filenameFormat = "{0:ddMMyyyy_HHmmss}.png";

        private GameObject prefabPhoto;
        private const string imagePath = "Photo/PhotoBackground/Image";
        private GameObject coroutineGameObject;
        private const string coroutineGameObjectName = "Coroutine";

        public PhotoController()
        {
            prefabPhoto = Resources.Load<GameObject>(ResourcesPathes.photoCanvas);
            path = Application.persistentDataPath;
            isProcessed = false;
        }

        private IEnumerator DoTapExampleAsync()
        {
            isProcessed = true;
            int sw = Screen.width;
            int sh = Screen.height;
            yield return new WaitForEndOfFrame();

            Texture2D sc = new Texture2D(sw, sh, TextureFormat.RGB24, true);

            sc.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);
            byte[] bytes = sc.EncodeToPNG();

            sc.LoadImage(bytes); // HELP QUESTION
            ShowPhoto(sc);

            string filename = String.Format(filenameFormat, DateTime.Now);
            File.WriteAllBytes(Path.Combine(path, filename), bytes);
            yield return new WaitForSeconds(1f);

            isProcessed = false;

            GameObject.Destroy(coroutineGameObject);


        }

        private void ShowPhoto(Texture2D texture)
        {
            GameObject canvas = GameObject.Instantiate(prefabPhoto);

            GameObject imageGameObject = canvas.transform.Find(imagePath).gameObject;
            Image image = imageGameObject.GetComponent<Image>();
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            GameObject.Destroy(canvas, 5f);
        }

        public void FirstMethod()
        {
            string filename = string.Format(filenameFormat, DateTime.Now);
            ScreenCapture.CaptureScreenshot(Path.Combine(path, filename), resolution);
        }

        private void TakeScreenShot()
        {
            if (isProcessed) return;

            coroutineGameObject = new GameObject(coroutineGameObjectName);
            CoroutineBehaviour coroutineBehaviour = coroutineGameObject.AddComponent<CoroutineBehaviour>();
            coroutineBehaviour.StartCoroutine(DoTapExampleAsync());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeScreenShot();
            }
        }
    }
}
