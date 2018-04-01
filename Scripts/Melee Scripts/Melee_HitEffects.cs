using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Utility;

namespace BaseFramework
{
	public class Melee_HitEffects : MonoBehaviour 
	{

        private Melee_Master meleeMaster;
        public GameObject defaultHitEffect;
        public GameObject enemyHitEffect;


		void OnEnable()
		{
            SetInitialReferences();
            meleeMaster.EventHit += SpawnHitEffects;
		}

		void OnDisable()
		{
            meleeMaster.EventHit -= SpawnHitEffects;
        }

		void SetInitialReferences()
		{
            meleeMaster = GetComponent<Melee_Master>();
		}

		void SpawnHitEffects(Collision hitCol, Transform hitTransform)
        {
            Quaternion quatAngle = Quaternion.LookRotation(hitCol.contacts[0].normal);
            HitEffects_Master hitMaster = hitTransform.root.GetComponent<HitEffects_Master>();

            if (hitMaster != null)
            {
                GameObject customHitEffect = hitMaster.hitEffects;
                AudioClip customHitAudio = hitMaster.hitAudio;

                if (customHitEffect != null)
                {
                    GameObject go = Instantiate(customHitEffect, hitCol.contacts[0].point, quatAngle);

                    //To destroy particles if any.
                    if (go.GetComponent<ParticleSystem>() != null)
                    {
                        go.AddComponent<ParticleSystemDestroyer>();
                        go.GetComponent<ParticleSystemDestroyer>().maxDuration = hitMaster.heWaitTime;
                    }

                    go.SetActive(true);
                }

                if (customHitAudio != null) AudioSource.PlayClipAtPoint(customHitAudio, hitCol.contacts[0].point, hitMaster.hitVolume);
            }

            else if (hitTransform.root.GetComponent<NPC_TakeDamage>() != null)
            {
                if (enemyHitEffect != null)
                Instantiate(enemyHitEffect, hitCol.contacts[0].point, quatAngle);
            }

            else
            {
                if (defaultHitEffect != null)
                Instantiate(defaultHitEffect, hitCol.contacts[0].point, quatAngle);
            }
        }
	}
}
