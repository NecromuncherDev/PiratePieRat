using PPR.Core;
using System.Collections.Generic;
using UnityEngine;

namespace PPR.Game
{
	public class PPRCompassComponent : PPRMonoBehaviour
	{
        [SerializeField] private GameObject markerPrefab;
        [SerializeField] private float compassSize;

        private Dictionary<Vector3, (Transform, CompassMarkers)> compass = new();

        private PPRTweenMapMarkerComponent createdObject;
        Vector2 dir, dirFactored;

        private void Start()
        {
            AddListener(PPREvents.pickup_created, AddToCompass);
            AddListener(PPREvents.pickup_collected, RemoveFromCompass);
            AddListener(PPREvents.pickup_destroyed, RemoveFromCompass);
        }

        private void AddToCompass(object markerPosition)
        {
            var marker = (GameObject)markerPosition;
            compass.Add(marker.transform.position, (null, CompassMarkers.Unknown));
            CreateCompassMarker(marker.transform.position);
        }

        private void RemoveFromCompass(object markerObject)
        {
            var marker = (PPRPickupComponent)markerObject;
            Manager.PoolManager.ReturnPoolable(compass[marker.transform.position].Item1.gameObject.GetComponent<PPRPoolable>());
            compass.Remove(marker.transform.position);
        }

        private void LateUpdate()
        {
            PlaceAllMarkersOnCompass(); // Costly
        }

        private void PlaceAllMarkersOnCompass()
        {
            foreach (var pickup in compass)
            {
                compass[pickup.Key].Item1.position = transform.position + RepositionMarkerOnCompass(pickup.Key);
            }
        }

        private void CreateCompassMarker(Vector3 position)
        {
            createdObject = (PPRTweenMapMarkerComponent)Manager.PoolManager.GetPoolable(PoolNames.MapMarker);
            compass[position] = (createdObject.transform, compass[position].Item2);
            createdObject.Init();
        }

        private Vector3 RepositionMarkerOnCompass(Vector3 position, CompassMarkers marker = CompassMarkers.Unknown)
        {
            dir = (position - transform.position);
            dirFactored = dir.normalized * compassSize;
            
            if (dir.magnitude > dirFactored.magnitude)
            {
                dir = dirFactored;
            }

            return new Vector3(dir.x, dir.y, 1f);
        }

        private void OnDestroy()
        {
            RemoveListener(PPREvents.pickup_created, AddToCompass);
            RemoveListener(PPREvents.pickup_collected, RemoveFromCompass);
            RemoveListener(PPREvents.pickup_destroyed, RemoveFromCompass);
        }
    }

    public enum CompassMarkers
    {
        Unknown = 0,
    }

}