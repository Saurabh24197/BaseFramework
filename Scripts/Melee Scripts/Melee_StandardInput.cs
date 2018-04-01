using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Melee_StandardInput : MonoBehaviour
    {

        private Melee_Master meleeMaster;
        private Transform myTransform;
        public float nextSwing;
        public string attackButtonName = "Fire1";


        void Start()
        {
            SetInitialReferences();

        }

        void Update()
        {
            CheckIfWeaponShouldAttack();
        }

        void SetInitialReferences()
        {
            myTransform = transform;
            meleeMaster = GetComponent<Melee_Master>();
        }

        void CheckIfWeaponShouldAttack()
        {
            if ((Time.timeScale > 0) && myTransform.root.CompareTag(GameManager_References._playerTag)
                && !meleeMaster.isInUse)
            {
                if (Input.GetButton(attackButtonName) && (Time.time > nextSwing))
                {
                    nextSwing = Time.time + meleeMaster.swingRate;
                    meleeMaster.isInUse = true;

                    meleeMaster.CallEventPlayerInput();
                }
            }
        }
    }
}

