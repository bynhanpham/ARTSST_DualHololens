//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//

using UnityEngine;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Unity.InputModule;

namespace HoloToolkit.Sharing.Tests
{
    /// <summary>
    /// Class that handles spawning sync objects on keyboard presses, for the SpawningTest scene.
    /// </summary>
    public class SyncObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private PrefabSpawnManager spawnManager;

        [SerializeField]
        [Tooltip("Optional transform target, for when you want to spawn the object on a specific parent.  If this value is not set, then the spawned objects will be spawned on this game object.")]
        private Transform spawnParentTransform;

        private void Awake()
        {
            if (spawnManager == null)
            {
                Debug.LogError("You need to reference the spawn manager on SyncObjectSpawner.");
            }

            // If we don't have a spawn parent transform, then spawn the object on this transform.
            if (spawnParentTransform == null)
            {
                spawnParentTransform = transform;
            }
        }

        public void SpawnBasicSyncObject()
        {
            //Vector3 position = Random.onUnitSphere * 2;
            Vector3 position = Camera.main.transform.position;
            Debug.Log("Position is " + position);
            Quaternion rotation = Camera.main.transform.rotation;

            var spawnedObject = new SyncSpawnedObject();

            spawnManager.Spawn(spawnedObject, position, rotation, Camera.main.gameObject, "SpawnedObject", false);
        }
        public void SpawnBasicSyncObject(Vector3 pos)
        {
            //Vector3 position = Random.onUnitSphere * 2;
            Vector3 position = pos;
            Debug.Log("Position is " + position);
            Quaternion rotation = Camera.main.transform.rotation;

            var spawnedObject = new SyncSpawnedObject();

            spawnManager.Spawn(spawnedObject, position, rotation, Camera.main.gameObject, "SpawnedObject", false);
        }

        public void SpawnCustomSyncObject()
        {
            Debug.Log("Got into Spawn Custon Sync Obj");
            Vector3 position = Camera.main.transform.position;
            Debug.Log("Posiition of custom is " + position);
            Quaternion rotation = Camera.main.transform.rotation;

            Debug.Log("creating spawned Obj");
            var spawnedObject = new SyncSpawnTestSphere();
            //spawnedObject.TestFloat.Value = Random.Range(0f, 100f);

            Debug.Log("About to actually spawn");
            spawnManager.Spawn(spawnedObject, position, rotation, Camera.main.gameObject, "SpawnTestSphere", false);
            Debug.Log("Spawned?");
        }

        /// <summary>
        /// Deletes any sync object that inherits from SyncSpawnObject.
        /// </summary>
        public void DeleteSyncObject()
        {
            GameObject hitObject = GazeManager.Instance.HitObject;
            if (hitObject != null)
            {
                var syncModelAccessor = hitObject.GetComponent<DefaultSyncModelAccessor>();
                if (syncModelAccessor != null)
                {
                    var syncSpawnObject = (SyncSpawnedObject)syncModelAccessor.SyncModel;
                    spawnManager.Delete(syncSpawnObject);
                }
            }
        }
    }
}
