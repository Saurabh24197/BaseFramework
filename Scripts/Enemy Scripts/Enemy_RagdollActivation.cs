using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Enemy_RagdollActivation : MonoBehaviour 
    {

        private Enemy_Master enemyMaster;
        private Collider myCollider;
        private Rigidbody myRigidBody;

        void OnEnable()
        {
            SetInitialReferences();

            enemyMaster.EventEnemyDie += ActivateRagdoll;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDie -= ActivateRagdoll;
        }

        void SetInitialReferences()
        {
            enemyMaster = transform.root.GetComponent<Enemy_Master>();

            if (GetComponent<Collider>() != null)
            {
                myCollider = GetComponent<Collider>();
            }

            if (GetComponent<Rigidbody>() != null )
            {
                myRigidBody = GetComponent<Rigidbody>();
            }

            ////Edit: Redid it using Inspector since the Golem head started to fly off during enable state.
            ////New Code. Enable the Projection to make sure that enemy doesn't behave funny during death.
            //if (GetComponent<CharacterJoint>() != null)
            //{
            //    GetComponent<CharacterJoint>().enableProjection = true;
            //}
        }

        void ActivateRagdoll()
        {
            if (myRigidBody != null)
            {
                myRigidBody.isKinematic = false;
                myRigidBody.useGravity = true;

                //custom code
                myRigidBody.mass = 200f;
            }

            if (myCollider != null)
            {
                myCollider.isTrigger = false;
                myCollider.enabled = true;
            }
        }
    }
}
