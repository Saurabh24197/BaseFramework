using UnityEngine;
using System.Collections;

namespace BaseFramework
{
	public class Destructible_Sounds : MonoBehaviour 
	{

        private Destructible_Master destructibleMaster;

        public bool playNearPlayer = false;

        [Space]
        public float explosionVolume = 0.5f;
        public AudioClip explodingSound;

		void OnEnable()
		{
            SetInitialReferences();
            destructibleMaster.EventDestroyMe += PlayExplosionSound;

        }

		void OnDisable()
		{
            destructibleMaster.EventDestroyMe -= PlayExplosionSound;
        }

		void SetInitialReferences()
		{
            destructibleMaster = GetComponent<Destructible_Master>();
		}

		void PlayExplosionSound()
        {
            if (explodingSound != null)
            {
                if (playNearPlayer)
                {
                    AudioSource.PlayClipAtPoint(explodingSound, GameManager_References._player.transform.position, explosionVolume);
                }
                
                else AudioSource.PlayClipAtPoint(explodingSound, transform.position, explosionVolume);
            }
        }
	}
}

