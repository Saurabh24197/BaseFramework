using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Item_ConvexMeshManagement : MonoBehaviour
    {

        private Item_Master itemMaster;
        public MeshCollider[] meshColliders;
        public Rigidbody myRigidbody;

        public bool isSettled = true;
        private float checkRate = 0.2f;
        private float nextCheck;

        void OnEnable()
        {
            SetInitialReferences();
            itemMaster.EventObjectPickup += EnableMeshConvex;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= EnableMeshConvex;
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfIHaveSettled();
        }

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void CheckIfIHaveSettled()
        {
            if (Time.time > nextCheck && !isSettled)
            {
                nextCheck = Time.time + checkRate;

                if (Mathf.Approximately(myRigidbody.velocity.magnitude, 0) 
                    && !myRigidbody.isKinematic)
                {
                    isSettled = true;
                    DisableMeshConvexEnableIsKinematic();
                }
            }
        }

        void EnableMeshConvex()
        {
            isSettled = false;

            if (meshColliders.Length > 0)
            {
                foreach (MeshCollider subMC in meshColliders)
                {
                    subMC.convex = true;
                }
            }
        }
 
        void DisableMeshConvexEnableIsKinematic()
        {
            myRigidbody.isKinematic = true;

            if(meshColliders.Length > 0)
            {
                foreach (MeshCollider subMC in meshColliders)
                {
                    subMC.convex = false;
                }
            }
        }


    }

}
