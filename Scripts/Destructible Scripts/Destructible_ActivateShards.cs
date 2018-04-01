using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BaseFramework
{
	public class Destructible_ActivateShards : MonoBehaviour 
	{

        private Destructible_Master destructibleMaster;
        public string shardLayer = "Ignore Raycast";
        public GameObject shards;
		public GameObject shardParticles;
        private float myMass;

        public bool DestroyShards = false;
        [Tooltip("A (int)Range from 4 to destroyWaitTime")]
        public int destroyWaitTime = 10;

        void OnEnable()
		{
            SetInitialReferences();
            destructibleMaster.EventDestroyMe += ActivateShards;
        }

		void OnDisable()
		{
            destructibleMaster.EventDestroyMe -= ActivateShards;
        }

		void SetInitialReferences()
		{
            destructibleMaster = GetComponent<Destructible_Master>();

            if (GetComponent<Rigidbody>() != null)
            {
                myMass = GetComponent<Rigidbody>().mass; 
            }
		}

		void ActivateShards()
        {
            if (shards != null)
            {
                shards.transform.parent = null;
                shards.SetActive(true);

                foreach (Transform shard in shards.transform)
                {
                    shard.tag = "Untagged";
                    shard.gameObject.layer = LayerMask.NameToLayer(shardLayer);

                    shard.GetComponent<Rigidbody>().AddExplosionForce(myMass, transform.position, 40, 0, ForceMode.Impulse);

                    if (DestroyShards)
                    {
                        Destroy(shard.gameObject, Random.Range(4, destroyWaitTime));
                    }
                }
            }

            if (shardParticles != null)
            {
                shardParticles.SetActive(true);

                Ray ray = new Ray(transform.position, Vector3.down);
                RaycastHit hitInfo;

                //Debug.DrawRay(transform.position, Vector3.down,Color.red, 10);
                Physics.Raycast(ray, out hitInfo, 1f);


                // 0.3F Displacement added to set the ground with Pivot.
                Vector3 particlePos = new Vector3(transform.position.x, transform.position.y - hitInfo.distance + .3f, transform.position.z);
                GameObject particle = Instantiate(shardParticles, particlePos, Quaternion.identity);

                if (DestroyShards)
                {
                    Destroy(particle, destroyWaitTime);
                }
            }
        }


	}
}

