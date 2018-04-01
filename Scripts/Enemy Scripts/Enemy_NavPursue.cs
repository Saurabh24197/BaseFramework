using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Enemy_NavPursue : MonoBehaviour 
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
                TryToChaseTarget();
            }
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
            checkRate = Random.Range(0.1f, 0.2f);

            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }
        }

        void TryToChaseTarget()
        {
            if (enemyMaster.myTarget != null && myNavMeshAgent != null && !enemyMaster.isNavPaused)
            {
                myNavMeshAgent.SetDestination(enemyMaster.myTarget.position);

                if (myNavMeshAgent.remainingDistance > myNavMeshAgent.stoppingDistance)
                {
                    enemyMaster.CallEventEnemyWalking();
                    enemyMaster.isOnRoute = true;
                }
            }
        }

        void DisableThisScript()
        {
            if (myNavMeshAgent != null)
            {
                myNavMeshAgent.enabled = false;
            }

            this.enabled = false;

        }
    }
}
