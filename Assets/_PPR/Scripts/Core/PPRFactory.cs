using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PPR.Core
{
    public class PPRFactory
    {
        public void CreateAsync<T>(string name, Vector3 pos, Action<T> onCreated) where T : Object
        {
            
        }

        public void CreateAsync<T>(T origin, Vector3 pos, Action<T> onCreated) where T : Object
        {
            var clone = Object.Instantiate(origin, pos, Quaternion.identity);
            onCreated.Invoke(clone);
        }

        public void MultiCreate<T>(T origin, Vector3 pos, int amount, Action<List<T>> onCreated) where T : Object
        {
            List<T> createdObjects = new();

            for (int obj = 0; obj < amount; obj++)
            {
                CreateAsync(origin, pos, OnCreated);
            }

            void OnCreated(T createdObject)
            {
                createdObjects.Add(createdObject);

                if (createdObjects.Count == amount)
                {
                    onCreated?.Invoke(createdObjects);
                }
            }
        }
    }
}
