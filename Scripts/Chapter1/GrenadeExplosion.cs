using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class GrenadeExplosion : MonoBehaviour
    {
        private Collider[] hitCollider;
        private float destroyTime = 8;
        public float blastRadius;
        public float explosionPower;
        public LayerMask explosionLayers;
        //private Z0GameManager_EventMaster eventMaster_ScoreManagement;

        void Start()
        {
            //eventMaster_ScoreManagement = GameObject.Find("GameManager").GetComponent<Z0GameManager_EventMaster>();
        }


        void OnCollisionEnter(Collision col)
        {
            //Debug.Log(col.contacts[0].point.ToString());
            ExplosionWork(col.contacts[0].point);
            Destroy(gameObject);
        }

        void ExplosionWork(Vector3 explosionPoint)
        {
            hitCollider = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);

            foreach (Collider hitCol in hitCollider)
            {

                if (hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false; 
                }
                //Debug.Log(hitCol.gameObject.name);
                if ( hitCol.GetComponent<Rigidbody>() != null)
                {
                    hitCol.GetComponent<Rigidbody>().isKinematic = false;
                    hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
                }

                if (hitCol.CompareTag("Enemy"))
                {
                    //eventMaster_ScoreManagement.CallMyUpdateScoreEvent();
                    hitCol.gameObject.tag = "DeadEnemy";
                    Destroy(hitCol.gameObject, destroyTime);
                }
            }
        }
    }
}

