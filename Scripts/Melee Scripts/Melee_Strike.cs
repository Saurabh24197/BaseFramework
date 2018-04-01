using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Melee_Strike : MonoBehaviour
    {

        private Melee_Master meleeMaster;
        private float nextSwingTime;
        public int damage = 25;

        void Start()
        {
            SetInitialReferences();
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
            {
                return;
            }

            if (col.gameObject != GameManager_References._player
                && meleeMaster.isInUse
                && Time.time > nextSwingTime)
            {
                nextSwingTime = Time.time + meleeMaster.swingRate;
                col.transform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
                col.transform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);

                meleeMaster.CallEventHit(col, col.transform);
            }
    }

        void SetInitialReferences()
        {
            meleeMaster = GetComponent<Melee_Master>();
        }


    }
}

