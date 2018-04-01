using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BaseFramework
{
    public class SpawnerProximity : MonoBehaviour 
    {

        public GameObject[] objectToSpawn;
        public int numberToSpawn;
        public bool spawnWithoutProximity = false;
        public float proximity;
        

        private float checkRate;
        private float nextCheck;

        private Transform myTransform;
        private Transform playerTransform;
        private Vector3 spawnPosition;
        public List<Transform> waypoints;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {

            if (spawnWithoutProximity)
            {
                SpawnObjects();
                this.enabled = false;
                return;
            }

            CheckDistance();
        }

        void SetInitialReferences()
        {
            myTransform = transform;
            playerTransform = GameManager_References._player.transform;
            checkRate = Random.Range(0.8f, 1.2f);
        }

        void CheckDistance()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;

                float distanceOf = Vector3.Distance(myTransform.position, playerTransform.position);

                if (distanceOf < proximity)
                {
                    SpawnObjects();
                    this.enabled = false;
                }
            }
        }

        void SpawnObjects()
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                spawnPosition = myTransform.position + Random.insideUnitSphere * 5;
                //Instantiate(objectToSpawn, spawnPosition, myTransform.rotation);

                foreach (GameObject spawnObj in objectToSpawn)
                {
                    GameObject go = (GameObject)Instantiate(spawnObj, spawnPosition, myTransform.rotation);

                    if (waypoints.Count > 0)
                    {
                        if (go.GetComponent<NPC_StatePattern>() != null)
                        {
                            go.GetComponent<NPC_StatePattern>().waypoints = waypoints;
                        }
                    }
                }
            }
        }


    }
}
