using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
	public class Melee_Sounds : MonoBehaviour 
	{

        private Melee_Master meleeMaster;
        private Transform myTransform;

        public AudioClip swingSound;
        public AudioClip strikeSound;
        public float swingSoundVolume = 1f;
        public float strikeSoundVolume = 1f;

		void OnEnable()
		{
            SetInitialReferences();
            meleeMaster.EventHit += PlayStrikeSound;
            
        }

		void OnDisable()
		{
            meleeMaster.EventHit -= PlayStrikeSound;
        }

		void SetInitialReferences()
		{
            myTransform = transform;
            meleeMaster = GetComponent<Melee_Master>();
		}

		void PlaySwingSound()
        {
            //Called by Animation.
            if (swingSound != null)
            {
                AudioSource.PlayClipAtPoint(swingSound, myTransform.position, swingSoundVolume);
            }
        }

        void PlayStrikeSound(Collision dummyCol, Transform dummyTransform)
        {
            if (strikeSound != null)
            {
                AudioSource.PlayClipAtPoint(strikeSound, myTransform.position, strikeSoundVolume);
            }
        }
	}
}
