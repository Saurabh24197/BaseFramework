using UnityEngine;
using System.Collections;

namespace BaseFramework
{

	public class Gun_StandardInput : MonoBehaviour 
	{

        private Gun_Master gunMaster;
        private float nextAttack;
        public float attackRate = 0.5f;
        private Transform myTransform;

        public bool isAutomatic;
        public bool hasBurstFire;
        private bool isBurstFireActive;

        public string attackButtonName = "Fire1";
        public string reloadButtonName = "Reload";
        public string burstFireButtonName = "Fire2";

        [Header("Used with Functions that are attached to Gun_Draw Animation")]
        public bool gunDrawComplete;

		// Use this for initialization
		void Start () 
		{
            SetInitialReferences();
        }
	
		// Update is called once per frame
		void Update () 
		{
            CheckIfWeaponShouldAttack();

            if (hasBurstFire)
            {
                CheckForBurstFireToggle();
            }

            CheckForReloadRequest();
		}

		void SetInitialReferences()
		{

            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;

            //The player can attempt shooting Right-away
            gunMaster.isGunLoaded = true;

		}
        
		void CheckIfWeaponShouldAttack()
        {

            if (Time.time > nextAttack && Time.timeScale > 0
                && gunMaster.isReloading == false
                && gunDrawComplete
                && myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                if (isAutomatic && !isBurstFireActive)
                {
                    if (Input.GetButton(attackButtonName))
                    {
                        //Debug.Log("Full Auto");

                        AttemptAttack();
                    }
                }
                else if (isAutomatic && isBurstFireActive)
                {
                    if (Input.GetButtonDown(attackButtonName))
                    {
                        //Debug.Log("Burst Fire");
                        StartCoroutine(RunBurstFire());
                    }
                }
                else if (!isAutomatic)
                {
                    if (Input.GetButtonDown(attackButtonName))
                    {
                        AttemptAttack();
                    }
                }
            }
        }

        void AttemptAttack()
        {
            if (gunMaster.isReloading)
            {
                return;
            }

            nextAttack = Time.time + attackRate;

            if (gunMaster.isGunLoaded)
            {
                //Debug.Log("Shooty!");
                gunMaster.CallEventPlayerInput();
            }

            else
            {
                gunMaster.CallEventGunNotUsable();
            }
        }

        void CheckForReloadRequest()
        {
            if (Input.GetButtonDown(reloadButtonName) && (Time.timeScale > 0)
                && myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                    gunMaster.CallEventRequestReload();
            }
        }

        void CheckForBurstFireToggle()
        {
            if (Input.GetButtonDown(burstFireButtonName) && (Time.timeScale > 0)
                && myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                //Debug.Log("Burst Fire Toggled");

                isBurstFireActive = !isBurstFireActive;
                gunMaster.CallEventToggleBurstFire();
            }
        }

        IEnumerator RunBurstFire()
        {
            //Attempt Attack with a 20% discount on attack rate.
            AttemptAttack();
            yield return new WaitForSeconds(attackRate - (attackRate * 0.2f));
            AttemptAttack();
            yield return new WaitForSeconds(attackRate - (attackRate * 0.2f));
            AttemptAttack();
        }

        //Attached to Animation Events
        public void GunDrawStart()
        {
            gunDrawComplete = false;
        }

        public void GunDrawComplete()
        {
            gunDrawComplete = true;
        }


	}
}
