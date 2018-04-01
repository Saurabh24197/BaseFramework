using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BaseFramework;

/// <summary>
/// Deploy Master is called by MapGenerator.
/// 
/// </summary>

namespace ProceduralCave
{
    [RequireComponent(typeof(MapGenerator))]
    public class DeployMaster : MonoBehaviour
    {
        private MapGenerator mapGeneratorGO;
        private DeployCoordinates coord;

		[TextArea] public string scriptInfo = "Spawn GameObjects at the Coordinates \n[RequireComponent(typeof(MapGenerator))]";
        public bool killOnDeploy = false;
        private GameObject player;

        [Space]
        public List<SpawnStructure> spawnList;
        public List<SpawnAbsolutes> spawnAbs;



        // |---------------------------------------------------------| Begin
        private void OnEnable()
        {
            mapGeneratorGO = GetComponent<MapGenerator>();
            coord = GetComponent<MapGenerator>().coord;

            mapGeneratorGO.EventSpawnGO += Deploy;
        }

        private void OnDisable()
        {
            mapGeneratorGO.EventSpawnGO -= Deploy;
        }

        public void Deploy()
        {
            SetupPlayer();
            SpawnAbs_Deploy();

			//Add Coordinates Count to Script Info
			scriptInfo += "\nCurrent Coordinates = " + coord.Count;

            for (int i = 0, n = (coord.Count <= 3) ? 2 : 1; i < n; i++)
            {
                SpawnList_Deploy();
            }

            if (killOnDeploy)
            {
                Destroy(this);
            }

        }

        public void SpawnAbs_Deploy()
        {
            for (int coordI = 0; coordI < coord.Count; coordI++)
            {
                int x = coord.coordX[coordI];
                int y = coord.coordY[coordI];

                for (int spawnAbsI = 0; spawnAbsI < spawnAbs.Count; spawnAbsI++)
                {
					if (coordI > 0 & spawnAbs[spawnAbsI].onlyPlayerPos)
					{
						continue;
					}

                    if (coordI == 0 & spawnAbs[spawnAbsI].ignorePlayerPos)
                    {
                        continue;
                    }

                    foreach (GameObject go in spawnAbs[spawnAbsI].go_List)
                    {
                        if (go == null)
                        {
                            continue;
                        }

                        //Lets hope this works
                        Vector3 spawnPOS = (spawnAbs[spawnAbsI].useWallHeight) 
                            ? new Vector3(x + spawnAbs[spawnAbsI].posVector.x , gameObject.transform.position.y - coord.wallHeight + 0.1f, y + spawnAbs[spawnAbsI].posVector.z) 
                            : new Vector3(x, gameObject.transform.position.y, y) + spawnAbs[spawnAbsI].posVector;

                        go.transform.position = spawnPOS;
                        Instantiate(go);
                    }
                }


            }

        }

        private void SetupPlayer()
        {
            player = GameManager_References._player;

            if (player != null)
            {
                int x = coord.coordX[0];
                int y = coord.coordY[0];

                Vector3 pos = new Vector3(x, -coord.wallHeight + 1, y);
                player.transform.position = pos;

				//A Necessary Hack
				player.transform.rotation = new Quaternion(0,1,0,1);
            }
        }

        private void SpawnList_Deploy()
        {
            if (spawnList.Count == 0)
            {
                return;
            }

			//Start Deploying NPC from the Second Coordinate.
            for (int coordI = (coord.Count > 1) ? 1 : 0; coordI < coord.Count; coordI++)
            {
                int spawnI = Random.Range(0, spawnList.Count);

                int x = (spawnList[spawnI].nearPlayer) ? coord.coordX[0] + Random.Range(-2, 3) : coord.coordX[coordI];
                int y = (spawnList[spawnI].nearPlayer) ? coord.coordY[0] + Random.Range(-2, 3) : coord.coordY[coordI];

                Vector3 spawnPOS = new Vector3(x, -coord.wallHeight + 1, y);

                //Instanciate GameObjects
                foreach (GameObject go in spawnList[spawnI].go_List)
                {
                    if (go == null)
                    {
                        continue;
                    }

                    if (coordI > 2)
                    {
                        //ShortHand && Operator. OH BOY!
                        if (spawnList[spawnI].randomizeGO & Random.value > 0.5f)
                        {
                            continue;
                        }

                    }

                    go.transform.position = spawnPOS;
                    Instantiate(go);
                }

				if (spawnList [spawnI].deleteEntry) 
				{
					spawnList.RemoveAt (spawnI);
				}
            }
        }




    }
}


