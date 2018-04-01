using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_SetLayer : MonoBehaviour 
    {

        private Item_Master itemMaster;
        [Header("[GameObject & ChildGO].SetLayer >> Item<PickUp|Throw>()")]
        public string itemThrowLayer = "Item";
        public string itemPickupLayer = "Weapon";
        
        void OnEnable()
        {
            SetInitialReferences();
            

            itemMaster.EventObjectPickup += SetItemToPickupLayer;
            itemMaster.EventObjectThrow += SetItemToThrowLayer;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= SetItemToPickupLayer;
            itemMaster.EventObjectThrow -= SetItemToThrowLayer;
        }

        void Start()
        {
            SetLayerOnEnable();
        }


        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void SetItemToThrowLayer()
        {
            SetLayer(transform, itemThrowLayer);
        }

        void SetItemToPickupLayer()
        {
            SetLayer(transform, itemPickupLayer);
        }

        void SetLayerOnEnable()
        {
            if (itemPickupLayer == "")
            {
                itemPickupLayer = "Item";
            }

            if (itemThrowLayer == "")
            {
                itemThrowLayer = "Item";
            }

            if (transform.root.CompareTag(GameManager_References._playerTag))
            {
                SetItemToPickupLayer();
            }
            else
            {
                SetItemToThrowLayer();
            }
        }

        void SetLayer(Transform tForm, string itemLayerName)
        {
            tForm.gameObject.layer = LayerMask.NameToLayer(itemLayerName);

            foreach ( Transform child in tForm)
            {
                SetLayer(child, itemLayerName);
            }
        }
    }
}
