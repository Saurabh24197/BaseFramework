using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_Sounds : MonoBehaviour 
    {

        private Item_Master itemMaster;

        public float defaultThrowVolume;
        public float defaultPickupVolume;

        public AudioClip throwSound;
        public AudioClip pickupSound;
        
        void OnEnable()
        {
            SetInitialReferences();
            itemMaster.EventObjectThrow += PlayThrowSound;
            itemMaster.EventObjectPickup += PlayPickUpSound;
        }

        void OnDisable()
        {
            itemMaster.EventObjectThrow -= PlayThrowSound;
            itemMaster.EventObjectPickup -= PlayPickUpSound;
        }

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void PlayThrowSound()
        {
            if (throwSound != null)
            {
                AudioSource.PlayClipAtPoint(throwSound, transform.position, defaultThrowVolume);
            }
        }

        void PlayPickUpSound()
        {
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, defaultPickupVolume);
            }
        }
    }
}
