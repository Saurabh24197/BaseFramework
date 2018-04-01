using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_Colliders : MonoBehaviour 
    {
        private Item_Master itemMaster;

        [Header("<Colliders>.Enabled >> Item<Pickup|Throw>()")]
        public Collider[] colliders;
        public PhysicMaterial myPhysicsMaterial;
        
        void OnEnable()
        {
            SetInitialReferences();

            itemMaster.EventObjectThrow += EnableColliders;
            itemMaster.EventObjectPickup += DisableColliders;
        }

        void OnDisable()
        {
            itemMaster.EventObjectThrow -= EnableColliders;
            itemMaster.EventObjectPickup -= DisableColliders;
        }

        void Start()
        {
            CheckIfStartsInInventory();
        }

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void CheckIfStartsInInventory()
        {
            if ( transform.root.CompareTag(GameManager_References._playerTag))
            {
                DisableColliders();
            }
        }

        void EnableColliders()
        {
            if ( colliders.Length > 0)
            {
                foreach ( Collider col in colliders)
                {
                    col.enabled = true;

                    if ( myPhysicsMaterial != null)
                    {
                        col.material = myPhysicsMaterial;
                    }


                }
            }
        }

        void DisableColliders()
        {
            if ( colliders.Length > 0)
            {
                foreach ( Collider col in colliders)
                {
                    col.enabled = false;

                }
            }
        }

    }
}
