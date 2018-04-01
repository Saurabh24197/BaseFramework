using UnityEngine;
using System.Collections;

namespace BaseFramework
{

	public class Gun_Ammo : MonoBehaviour 
	{

        private Player_Master playerMaster;
        private Gun_Master gunMaster;
        private Player_AmmoBox ammoBox;
        private Animator myAnimator;

        public int clipSize;
        public int currentAmmo;
        public string ammoName;
        public float reloadTime;

	    
		void OnEnable()
		{
            SetInitialReferences();
            StartingSanityCheck();
            CheckAmmoStatus();

            gunMaster.EventPlayerInput += DeductAmmo;
            gunMaster.EventPlayerInput += CheckAmmoStatus;
            gunMaster.EventRequestReload += TryToReload;
            gunMaster.EventGunNotUsable += TryToReload;
            gunMaster.EventRequestGunReset += ResetGunReloading;

            if (playerMaster != null)
            {
                playerMaster.EventAmmoChanged += UIAmmoUpdateRequest;
            }

            if (ammoBox != null)
            {
                StartCoroutine(UpdateAmmoUIWhenEnabling());
            }

            if (gunMaster.isReloading)
            {
                ResetGunReloading();
            }
		}

		void OnDisable()
		{
            gunMaster.EventPlayerInput -= DeductAmmo;
            gunMaster.EventPlayerInput -= CheckAmmoStatus;
            gunMaster.EventRequestReload -= TryToReload;
            gunMaster.EventGunNotUsable -= TryToReload;
            gunMaster.EventRequestGunReset -= ResetGunReloading;

            if (playerMaster != null)
            {
                playerMaster.EventAmmoChanged -= UIAmmoUpdateRequest;
            }
        }

		// Use this for initialization
		void Start () 
		{
            SetInitialReferences();
            StartCoroutine(UpdateAmmoUIWhenEnabling());
        }

		void SetInitialReferences()
		{
            gunMaster = GetComponent<Gun_Master>();

            if (GetComponent<Animator>() != null)
            {
                myAnimator = GetComponent<Animator>();
            }

            if (GameManager_References._player != null)
            {
                playerMaster = GameManager_References._player.GetComponent<Player_Master>();
                ammoBox = GameManager_References._player.GetComponent<Player_AmmoBox>();
            }
		}
        
		void DeductAmmo()
        {
            currentAmmo--;
            UIAmmoUpdateRequest();
        }

        void TryToReload()
        {
            for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++)
            {
                if (ammoBox.typesOfAmmunition[i].ammoName == ammoName)
                {

                    if (ammoBox.typesOfAmmunition[i].ammoCurrentCarried > 0
                        && currentAmmo != clipSize
                        && !gunMaster.isReloading)
                    {
                        gunMaster.isReloading = true;
                        gunMaster.isGunLoaded = false;
                    }

                    else if (ammoBox.typesOfAmmunition[i].ammoCurrentCarried == 0)
                    {
                        gunMaster.isReloading = false;
                        break;
                    }

                    else if (ammoBox.typesOfAmmunition[i].ammoCurrentCarried == 0 && currentAmmo == 0)
                    {
                        gunMaster.isGunLoaded = false;
                        break;
                    }
                    else if (currentAmmo == clipSize)
                    {
                        gunMaster.isGunLoaded = true;
                        gunMaster.isReloading = false;
                        break;
                    }



                    if (myAnimator != null)
                    {
                        myAnimator.SetTrigger("Reload");

                    }
                    else
                    {
                        StartCoroutine(ReloadWithoutAnimation());
                    }
                    break;
                }        
            }
        }

        void CheckAmmoStatus()
        {
            if (currentAmmo <= 0)
            {
                currentAmmo = 0;
                gunMaster.isGunLoaded = false;
            }
            else if (currentAmmo > 0)
            {
                gunMaster.isGunLoaded = true;
            }
        }

        void StartingSanityCheck()
        {
            if (currentAmmo > clipSize)
            {
                currentAmmo = clipSize;
            }
        }

        void UIAmmoUpdateRequest()
        {
            for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++)
            {
                if (ammoBox.typesOfAmmunition[i].ammoName == ammoName)
                {
                    gunMaster.CallEventAmmoChanged(currentAmmo, ammoBox.typesOfAmmunition[i].ammoCurrentCarried);
                    break;
                }
            }
        }

        void ResetGunReloading()
        {
            gunMaster.isReloading = false;
            CheckAmmoStatus();
            UIAmmoUpdateRequest();
        }

        public void OnReloadComplete()
        {
            //Called by Reload Animation
            //Attempt to add ammo to current Gun.
            for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++)
            {
                if (ammoBox.typesOfAmmunition[i].ammoName == ammoName)
                {
                    int ammoTopUp = clipSize - currentAmmo;

                    if (ammoBox.typesOfAmmunition[i].ammoCurrentCarried >= ammoTopUp)
                    {
                        currentAmmo += ammoTopUp;
                        ammoBox.typesOfAmmunition[i].ammoCurrentCarried -= ammoTopUp;
                    }
                    else if(ammoBox.typesOfAmmunition[i].ammoCurrentCarried < ammoTopUp
                        && ammoBox.typesOfAmmunition[i].ammoCurrentCarried != 0)
                    {
                        currentAmmo += ammoBox.typesOfAmmunition[i].ammoCurrentCarried;
                        ammoBox.typesOfAmmunition[i].ammoCurrentCarried = 0;
                    }
                    break;
                }
            }
            ResetGunReloading();
        }


        IEnumerator ReloadWithoutAnimation()
        {
            yield return new WaitForSeconds(reloadTime);
            OnReloadComplete();


        }

        IEnumerator UpdateAmmoUIWhenEnabling()
        {
            //This is a fudge factor to ensure that the UI is updated when changing weapons.
            yield return new WaitForSeconds(0.05f);

            UIAmmoUpdateRequest();

        }
	}
}
