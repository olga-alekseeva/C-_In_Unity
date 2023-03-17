using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oliasca.Maze
{
    public sealed class RadarController : IUpdate
    {
        private const string maskPath = "Radar/RadarBackground/Mask";
        private GameObject radarCanvas;
        private GameObject radarParentMask;
        private GameObject radarObjectPrefab;
        private GameObject player;
        private List<IRadarObjectSource> radarObjectSources;
        private List<RadarObject> radarObjects;
        private float mapScale;

        public RadarController(GameObject player)
        {
            mapScale = 3f;
            this.player = player;
            radarObjectSources = new List<IRadarObjectSource>();
            radarObjects = new List<RadarObject>();
            radarObjectPrefab = Resources.Load<GameObject>(ResourcesPathes.radarIcon);
            Show();
        }

        private void Show()
        {
            GameObject radarCanvasPrefab = Resources.Load<GameObject>(ResourcesPathes.radarCanvas);
            radarCanvas = GameObject.Instantiate(radarCanvasPrefab);
            Transform mask = radarCanvas.transform.Find(maskPath);
            if (mask == null) throw new Exception($"Radar have no mask path {maskPath}");
            radarParentMask = mask.gameObject;
        }

        public void AddRadarObjectSource(IRadarObjectSource radarObjectSource)
        {
            radarObjectSources.Add(radarObjectSource);
            radarObjectSource.eventAddRadarObject += AddRadarObject;
            radarObjectSource.eventRemoveRadarObject += RemoveRadarObject;
        }

        private void AddRadarObject(GameObject gameObject)
        {
            GameObject radarGameObject = GameObject.Instantiate(radarObjectPrefab);
            radarGameObject.transform.SetParent(radarParentMask.transform);
            radarObjects.Add(new RadarObject(gameObject, radarGameObject));
        }

        private void RemoveRadarObject(GameObject gameObject)
        {
            for (int i = 1; i <= radarObjects.Count; i++)
            {
                int index = radarObjects.Count - i;
                if (radarObjects[index].radarObject == gameObject)
                {
                    GameObject.Destroy(radarObjects[index].radarImage);
                    radarObjects.Remove(radarObjects[index]);
                }
            }
            
        }

        public void Update(float deltaTime)
        {
            foreach (RadarObject radarObject in radarObjects)
            {
                Vector3 radarPos = (radarObject.radarObject.transform.position - player.transform.position);
                float distToObject = Vector3.Distance(player.transform.position, radarObject.radarObject.transform.position) * mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
                radarObject.radarImage.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + radarParentMask.transform.position;
            }

        }
    }
}
