using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BaseFramework
{

    public class Player_AmmoBox : MonoBehaviour 
    {
        private Player_Master playerMaster;

        [System.Serializable]
        public class AmmoTypes
        {
            public string ammoName;
            public int ammoMaxQty;
            public int ammoCurrentCarried;

            public AmmoTypes(string aName, int aMaxQty, int aCurrentCarried)
            {
                ammoName = aName;
                ammoMaxQty = aMaxQty;
                ammoCurrentCarried = aCurrentCarried;
            }
        }

        public List<AmmoTypes> typesOfAmmunition = new List<AmmoTypes>();


        void OnEnable()
        {
            SetInitialReferences();
            playerMaster.EventPickedUpAmmo += PickedUpAmmo;
        }

        void OnDisable()
        {
            playerMaster.EventPickedUpAmmo -= PickedUpAmmo;
        }

        void SetInitialReferences()
        {
            playerMaster = GetComponent<Player_Master>();
        }

        void PickedUpAmmo(string ammoName, int quantity)
        {
            for ( int i = 0; i < typesOfAmmunition.Count; i++ )
            {
                if ( typesOfAmmunition[i].ammoName == ammoName)
                {

                    typesOfAmmunition[i].ammoCurrentCarried += quantity;
                    
                    if ( typesOfAmmunition[i].ammoCurrentCarried > typesOfAmmunition[i].ammoMaxQty)
                    {
                        typesOfAmmunition[i].ammoCurrentCarried = typesOfAmmunition[i].ammoMaxQty;
                    }

                    playerMaster.CallEventAmmoChanged();

                    break;
                }
            }
        }
    }
}

