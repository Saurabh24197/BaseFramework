using UnityEngine;
using System.Collections;

namespace BaseFramework
{
	public class Item_Medkit : MonoBehaviour 
	{
        private Player_Master playerMaster;

        private int playerHealth;
        public int healthIncrement;

        public AudioClip healthGainedAudio;
        //public AudioClip bounceAudio;

        void OnTriggerEnter(Collider other)
		{
            SetInitialReferences();

            if (other.transform.root.CompareTag(GameManager_References._playerTag))
            {
                if (playerHealth == playerMaster.GetComponent<Player_Health>().maxPlayerHealth)
                {
                    return;
                }

                else
                {
                    AudioSource.PlayClipAtPoint(healthGainedAudio, transform.position);
                    playerMaster.CallEventPlayerHealthIncrease(healthIncrement);
                    Destroy(transform.root.gameObject);
                }           
            }
		}

		void SetInitialReferences()
		{
            //Added for a unknown reason, because of Script Execution Order's 
            //late initialization of GameManager_References or something(._.).
            if (GameManager_References._player != null) 
            {
                playerMaster = GameManager_References._player.GetComponent<Player_Master>();
                playerHealth = playerMaster.GetComponent<Player_Health>().playerHealth;
            }  
		}

		
	}
}

