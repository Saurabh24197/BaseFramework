using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_Ammo : MonoBehaviour 
    {

        private Item_Master itemMaster;
        private GameObject playerGo;


        public string ammoName;
        public int quantity;
        public bool isTriggerPickup;

        public AudioClip pickupAudio;

        
        void OnEnable()
        {
            SetInitialReferences();
            itemMaster.EventObjectPickup += TakeAmmo;
        }

        void OnDisable()
        {
            itemMaster.EventObjectPickup -= TakeAmmo;
        }

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameManager_References._playerTag) && isTriggerPickup)
            {
                TakeAmmo();
            }

            if (other.CompareTag(GameManager_References._playerTag))
            {
                TakeAmmo();
            }
        }

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
            playerGo = GameManager_References._player;

            if (isTriggerPickup)
            {
                if (GetComponent<Collider>() != null)
                {
                    GetComponent<Collider>().isTrigger = true; 
                }

                if (GetComponent<Rigidbody>() != null)
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }

        void TakeAmmo()
        {
            playerGo.GetComponent<Player_Master>().CallEventPickUpAmmo(ammoName, quantity);
            
			if (pickupAudio != null)
			    AudioSource.PlayClipAtPoint(pickupAudio, transform.position);

			Destroy(gameObject.transform.root.gameObject);
        }
    }
}
