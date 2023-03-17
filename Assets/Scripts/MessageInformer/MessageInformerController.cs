using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oliasca.Maze
{
    public sealed class MessageInformerController : IDisposable, IUnscaledUpdate
    {
        private GameObject parentCanvasPrefab;
        private GameObject messagePrefab;
        private GameObject parentCanvas;
        private bool parentCanvasVisible;
        private List<IMessageEventSource> listSources;
        private List<MessageMatch> listMessages;

        public MessageInformerController()
        {
            parentCanvasPrefab = Resources.Load<GameObject>(ResourcesPathes.canvasInformerPrefab);
            messagePrefab = Resources.Load<GameObject>(ResourcesPathes.messagePrefab);
            listSources = new List<IMessageEventSource>();
            listMessages = new List<MessageMatch>();
            parentCanvas = null;
            parentCanvasVisible = false;
        }

        public void NewMessage(IMessage message)
        {
            MessageMatch messageMatch = new MessageMatch(message, new Timer(3f));
            listMessages.Insert(0, messageMatch);
        }

        private void ShowCanvas()
        {
            if (!parentCanvasVisible)
            {
                parentCanvas = GameObject.Instantiate(parentCanvasPrefab, Vector3.zero, Quaternion.identity);
                parentCanvasVisible = true;
            }
        }
        private void HideCanvas()
        {
            if (parentCanvasVisible)
            {
                GameObject.Destroy(parentCanvas);
                parentCanvasVisible = false;
            }
        }

        public float ChangeFloat(float start, float end, float maxChange)
        {
            float change = Mathf.Abs(start - end);
            if (change > maxChange) change = maxChange;
            float nextValue;
            if (start > end) nextValue = start - change; else nextValue = start + change;
            return nextValue;
        }

        private void ShowMessage(MessageMatch messageMatch, Vector3 targetPosition)
        {
            messageMatch.gameObject = GameObject.Instantiate(messagePrefab, targetPosition, Quaternion.identity);
            messageMatch.gameObject.transform.SetParent(parentCanvas.transform);
            messageMatch.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            messageMatch.rectTransform = messageMatch.gameObject.GetComponent<RectTransform>();
            Vector2 offsetMax = messageMatch.rectTransform.offsetMax;
            offsetMax.x = 0;
            messageMatch.rectTransform.offsetMax = offsetMax;
            Text text = messageMatch.gameObject.GetComponent<Text>();
            text.text = messageMatch.message.text;
            messageMatch.visible = true;
        }

        public void UnscaledUpdate(float unscaledDeltaTime) 
        {

            if (listMessages.Count > 0) ShowCanvas();

            float yPosition = 0f;
            
            foreach(MessageMatch messageMatch in listMessages)
            {
                messageMatch.timer.Update(unscaledDeltaTime);

                //Создание
                Vector3 targetPosition = new Vector3(0f, yPosition, 0f);
                if (!messageMatch.visible)
                {
                    ShowMessage(messageMatch, targetPosition);
                }

                //Перемещение

                float newYPosition = ChangeFloat(messageMatch.gameObject.transform.position.y, targetPosition.y, unscaledDeltaTime * 30f);
                messageMatch.gameObject.transform.position = new Vector3(0f, newYPosition, 0f);

                //Увеличение и уменьшение
                float targetScale = 0f;
                if (messageMatch.timer.On) targetScale = 1f;
                float currentScale = ChangeFloat(messageMatch.gameObject.transform.localScale.x, targetScale, unscaledDeltaTime);
                messageMatch.gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

                yPosition += 30f;
            }

            //Удаление сообщений
            for(int i = 1; i <= listMessages.Count; i++)
            {
                int index = listMessages.Count - 1;
                MessageMatch messageMatch = listMessages[index];
                if (messageMatch.gameObject.transform.localScale.x == 0f)
                {
                    GameObject.Destroy(messageMatch.gameObject);
                    messageMatch.gameObject = null;
                    messageMatch.visible = false;
                    listMessages.Remove(messageMatch);
                }
            }

            if (listMessages.Count == 0) HideCanvas();
        }

        public void AddMessageSource(IMessageEventSource messageEventSource)
        {
            messageEventSource.actionMessage += NewMessage;
            listSources.Add(messageEventSource);
        }

        public void Dispose()
        {
            foreach(IMessageEventSource messageEventSource in listSources)
            {
                if (messageEventSource != null) messageEventSource.actionMessage -= NewMessage;
            }
            listSources.Clear();
        }
    }
}
