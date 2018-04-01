using UnityEngine;
using System.Collections;

namespace BaseFramework
{

    public class Player_DetectItem : MonoBehaviour 
    {
        [Tooltip("What layer is being used for Items.")]
        public LayerMask layerToDetect;

        [Tooltip("What Transform will the ray be fired from?")]
        public Transform rayTransformPivot;

        [Tooltip("The Editor's Input button that will be used for picking up Items.")]
        public string buttonPickup;

        private Transform itemAvailableForPickup;
        private RaycastHit hit;
        private float detectRange = 3;
        private float detectRadius = 0.7f;
        private bool itemInRange;

        private float labelWidth = 200;
        private float labelHeight = 50;

        void Update()
        {
            CastRayForDetectingItems();
            CastRayForPickUpItems();
        }

        void CastRayForDetectingItems()
        {
            if (Physics.SphereCast(rayTransformPivot.position, detectRadius, rayTransformPivot.forward, out hit, detectRange, layerToDetect))
            {
                itemAvailableForPickup = hit.transform;
                itemInRange = true;

            }else
            {
                itemInRange = false;
            }

        }

        void CastRayForPickUpItems()
        {
            if ( Input.GetButtonDown(buttonPickup) && Time.timeScale > 0 && itemInRange && itemAvailableForPickup.root.tag != GameManager_References._playerTag)
            {
                //Debug.Log("Item PickUp attempt = true");
                itemAvailableForPickup.GetComponent<Item_Master>().CallEventPickupAction(rayTransformPivot);
            }
        }


        void OnGUI()
        {
            if ( itemInRange && itemAvailableForPickup != null )
            {
                
                GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2 - labelHeight / 2, labelWidth, labelHeight), itemAvailableForPickup.name);

            }
        }


       
    }
}

