using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Enemy_Attack : MonoBehaviour 
    {

        private Enemy_Master enemyMaster;
        private Transform attackTarget;
        private Transform myTransform;
        public float attackRate = 1;
        private float nextAttack;
        public float attackRange = 3.5f;
        public int attackDamage = 3;

        
        void OnEnable()
        {
            SetInitialReferences();

            enemyMaster.EventEnemyDie += DisableThisScript;
            enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= DisableThisScript;
            enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
        }

        // Update is called once per frame
        void Update()
        {

            TryToAttack();
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
            myTransform = transform;
        }

        void SetAttackTarget(Transform targetTransform)
        {
            attackTarget = targetTransform;
        }

        void TryToAttack()
        {

            //below nested if
            if (attackTarget != null && (Time.time > nextAttack))
            {
                nextAttack = Time.time + attackRate;

                if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange)
                {
                    Vector3 lookAtVector = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
                    myTransform.LookAt(lookAtVector);
                    enemyMaster.CallEventEnemyAttack();
                    enemyMaster.isOnRoute = false;
                }
            }
        }

        //Called by hPunch animation
        public void OnEnemyAttack()
        {
            float presentAttackRange;
            presentAttackRange = Vector3.Distance(myTransform.position, attackTarget.position);
         
            if (attackTarget != null)
            {

                if ((presentAttackRange <= attackRange) 
                    && (attackTarget.GetComponent<Player_Master>() != null))
                {
                    Vector3 toOther = attackTarget.position - myTransform.position;
                    //Debug.Log(Vector3.Dot(toOther, myTransform.forward).ToString());

                    if (Vector3.Dot(toOther, myTransform.forward) > 0.5f)
                    {
                        attackTarget.GetComponent<Player_Master>().CallEventPlayerHealthDeduction(attackDamage);
                    }
                }
            }
        }

        void DisableThisScript()
        {
            this.enabled = false;
        }
    }
}
