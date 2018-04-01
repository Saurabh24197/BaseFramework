using UnityEngine;
using System.Collections;

namespace BaseFramework
{
	public class Destructible_IntermediaryEffects : MonoBehaviour 
	{

        private Destructible_Master destructibleMaster;
        public GameObject[] intermediaryEffectsGo;
        public AudioClip[] intermediarySFX;
        public float sfxVolume = .5f;

		void OnEnable()
		{
            SetInitialReferences();
            destructibleMaster.EventHealthLow += TurnOnEffects;
            destructibleMaster.EventHealthLow += TurnOnSFX;

        }

		void OnDisable()
		{
            destructibleMaster.EventHealthLow -= TurnOnEffects;
            destructibleMaster.EventHealthLow -= TurnOnSFX;
        }

		void SetInitialReferences()
		{
            destructibleMaster = GetComponent<Destructible_Master>();
        }

		void TurnOnEffects()
        {
            if (intermediaryEffectsGo.Length > 0)
            {
                foreach (GameObject go in intermediaryEffectsGo)
                {
                    go.SetActive(true);
                }
            }
        }

        void TurnOnSFX()
        {
            if (intermediarySFX.Length > 0)
            {
                foreach (AudioClip aClip in intermediarySFX)
                {
                    if (aClip != null)
                    AudioSource.PlayClipAtPoint(aClip, transform.position, sfxVolume);  
                }
            }
        }
	}
}

