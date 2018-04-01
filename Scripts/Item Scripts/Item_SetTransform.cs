using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_SetTransform : MonoBehaviour 
    {

        private Item_Master itemMaster;

        public Vector3 itemLocalPosition;

        public bool setRotation;
        public Vector3 itemLocalRotation;

        public bool setScale;
        public Vector3 itemLocalScale;

        
        void OnEnable()
        {
            SetInitialReferences();
            

            itemMaster.EventObjectPickup += SetPositionOnPlayer;

        }

        void OnDisable()
        {
            
            itemMaster.EventObjectPickup -= SetPositionOnPlayer;
        }

        void Start()
        {
            SetPositionOnPlayer();
        }

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
            
        }

        void SetPositionOnPlayer()
        {
            if (transform.root.CompareTag(GameManager_References._playerTag))
            {
                transform.localPosition = itemLocalPosition;

                if(setRotation)
                transform.localEulerAngles = itemLocalRotation;

                
                if (setScale && (itemLocalScale != Vector3.zero))
                transform.localScale = itemLocalScale;
            }
        }
    }
}
