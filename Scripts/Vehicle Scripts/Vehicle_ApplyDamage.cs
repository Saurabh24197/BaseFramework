﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Vehicle_ApplyDamage : MonoBehaviour
    {

        [Header("Add 2 Instances for Forward and Backward")]
        public Transform rayTransformPivot;
        private RaycastHit hit;
        public float range = 1.7f;
        private Vector3 boxHalfExtents;
        private Rigidbody myRigidbody;
        private int damageToApply;
        private float sqrVelocityThreshold = 25;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            BoxCastAhead();
        }

        void SetInitialReferences()
        {
            boxHalfExtents = new Vector3(1.5f, 0.25f, 0.5f);
            myRigidbody = GetComponent<Rigidbody>();
        }

        void BoxCastAhead()
        {
            if (myRigidbody.velocity.sqrMagnitude < sqrVelocityThreshold)
            {
                return;
            }

            if (Physics.BoxCast(rayTransformPivot.position, boxHalfExtents,
                rayTransformPivot.forward, out hit, rayTransformPivot.rotation, range))
            {
                damageToApply = (int)(myRigidbody.velocity.sqrMagnitude * 1.1f);

                if (hit.transform.GetComponent<Destructible_TakeDamage>() != null)
                {
                    hit.transform.GetComponent<Destructible_TakeDamage>().ProcessDamage(damageToApply);
                    hit.transform.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
                }

                else if (hit.transform.root.GetComponent<NPC_TakeDamage>() != null)
                {
                    hit.transform.GetComponent<NPC_TakeDamage>().ProcessDamage(damageToApply);
                    hit.transform.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

}

