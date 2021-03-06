﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Melee_Swing : MonoBehaviour
    {

        private Melee_Master meleeMaster;
        public Collider myCollider;
        public Rigidbody myRigidbody;
        public Animator myAnimator;

        void OnEnable()
        {
            SetInitialReferences();
            meleeMaster.EventPlayerInput += MeleeAttackAction;
        }

        void OnDisable()
        {
            meleeMaster.EventPlayerInput -= MeleeAttackAction;
        }


        void SetInitialReferences()
        {
            meleeMaster = GetComponent<Melee_Master>();
        }

        void MeleeAttackAction()
        {
            myCollider.enabled = true;
            myRigidbody.isKinematic = false;
            myAnimator.SetTrigger("Attack");
        }

        void MeleeAttackComplete()
        {
            //Called by Animation
            myCollider.enabled = false;
            myRigidbody.isKinematic = true;
            meleeMaster.isInUse = false;
        }
    }
}

