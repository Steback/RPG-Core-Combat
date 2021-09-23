using System;
using UnityEngine;

namespace Core
{
    public class PersistentObject : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab;

        private static bool hasSpawon = false;
        
        private void Awake()
        {
            if (hasSpawon) return;

            SpawnPersistentObjects();
            
            hasSpawon = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
