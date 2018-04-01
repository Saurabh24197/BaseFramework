using UnityEngine;
using System.Collections;

namespace BaseFramework
{
	public class Collide_Noise : MonoBehaviour 
	{
        public AudioClip[] noiseClips;

        void OnTriggerEnter(Collider col)
        {
            for(int i=0; i < noiseClips.Length; i++)
            {
                AudioSource.PlayClipAtPoint(noiseClips[i], gameObject.transform.position);
            }
        }
	}
}

