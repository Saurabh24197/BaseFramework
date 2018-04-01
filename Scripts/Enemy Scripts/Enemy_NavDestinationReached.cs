using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Enemy_NavDestinationReached : MonoBehaviour 
    {

        private Enemy_Master enemyMaster;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private float checkRate;
        private float nextCheck;

        void OnEnable()
        {
            SetInitialReferences();
            enemyMaster.EventEnemyDie += DisableThisScript;

        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThisScript;
        }

        // Update is called once per frame
        void Update()
        {
    
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CheckIfDestinationReached();
            }
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<Enemy_Master>();

            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            }

            checkRate = Random.Range(0.3f, 0.4f);
        }

        void CheckIfDestinationReached()
        {
            if (enemyMaster.isOnRoute)
            {
                if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance)
                {
                    enemyMaster.isOnRoute = false;
                    enemyMaster.CallEventEnemyReachedNavTarget();
                }
            }
        }

        void DisableThisScript()
        {
            this.enabled = false;
        }
    }
}
