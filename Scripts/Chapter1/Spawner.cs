using UnityEngine;
using System.Collections;


namespace Chapter1
{
  
    public class Spawner : MonoBehaviour
    {
        public int numberOfEnemies;
        private float spawnRadius = 10f;
        public GameObject objectToSpawn;
        
        private Vector3 spawnPosition;

        private GameManager_EventMaster eventMasterScript;


        public void OnEnable()
        {
            SetInitialReferences();
            eventMasterScript.myGeneralEvent += SpawnObject;
        }

        void OnDisable()
        {
            SetInitialReferences();
            eventMasterScript.myGeneralEvent -= SpawnObject;
        }

        void SetInitialReferences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
        }

        void SpawnObject()
        {
            for (int i=0; i < numberOfEnemies; i++)
            {
                spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }
}

