using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_Throw : MonoBehaviour
    {
        private Item_Master itemMaster;
        private Transform myTransform;
        private Rigidbody myRigidbody;
        private Vector3 throwDirection;

        public bool canBeThrown = true;
        public string throwButtonName = "Throw";
        public string throwButtonNameAlt = "";
        public float throwForce;

        // Use this for initialization
        void Start()
        {
            if (!canBeThrown)
            {
                this.enabled = false;
                //Destroy(this);
            }

            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForThrowInput();
        }

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
            myTransform = transform;
            myRigidbody = GetComponent<Rigidbody>();

            //Set it to throwButtonName to prevent any Input Exceptions
            if (throwButtonNameAlt == "") throwButtonNameAlt = throwButtonName;
        }

        void CheckForThrowInput()
        {
            if ( throwButtonName != null)
            {
                if ( (Input.GetButtonDown(throwButtonName) || Input.GetButtonDown(throwButtonNameAlt))
                    && Time.timeScale > 0
                    && myTransform.root.CompareTag(GameManager_References._playerTag))
                {
                    CarryOutThrowAction();
                }
            }
        }

        void CarryOutThrowAction()
        {
            throwDirection = myTransform.parent.forward;
            myTransform.parent = null;

            itemMaster.CallEventObjectThrow();
            HurlItem();  
        }

        void HurlItem()
        {
            myRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }


    }
}
