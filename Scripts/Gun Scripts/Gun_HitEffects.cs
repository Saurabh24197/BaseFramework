using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityStandardAssets.Utility;

namespace BaseFramework
{

	public class Gun_HitEffects : MonoBehaviour 
	{

        private Gun_Master gunMaster;
        public GameObject defaultHitEffect;
        public GameObject enemyHitEffect;
	    
		void OnEnable()
		{
            SetInitialReferences();

            gunMaster.EventShotDefault += SpawnDefaultHitEffect;
            gunMaster.EventShotEnemy += SpawnEnemyHitEffect;
        }

		void OnDisable()
		{
            gunMaster.EventShotDefault -= SpawnDefaultHitEffect;
            gunMaster.EventShotEnemy -= SpawnEnemyHitEffect;
        }

		void SetInitialReferences()
		{
            gunMaster = GetComponent<Gun_Master>();
		}
        
		void SpawnDefaultHitEffect(RaycastHit hitPosition, Transform hitTransform)
        {
            HitEffects_Master hitMaster = hitPosition.transform.root.GetComponent<HitEffects_Master>();
            Quaternion quatAngle = Quaternion.LookRotation(hitPosition.normal);

            if (hitMaster != null)
            {
                SpawnCustomHitEffects(hitMaster, hitPosition, quatAngle);
                return;
            }

            if (defaultHitEffect != null)
            {
                Instantiate(defaultHitEffect, hitPosition.point, quatAngle);
            }
        }

        void SpawnEnemyHitEffect(RaycastHit hitPosition, Transform hitTransform)
        {
            HitEffects_Master hitMaster = hitPosition.transform.root.GetComponent<HitEffects_Master>();
            Quaternion quatAngle = Quaternion.LookRotation(hitPosition.normal);

            if (hitMaster != null)
            {
                SpawnCustomHitEffects(hitMaster, hitPosition, quatAngle);
                return;
            }

            //Default Enemy_HitEffects.
            if (enemyHitEffect != null)
            {
                Instantiate(enemyHitEffect, hitPosition.point, quatAngle);
            }
        }

        void SpawnCustomHitEffects(HitEffects_Master hitMaster, RaycastHit hitPosition, Quaternion quatAngle)
        {
            GameObject customHitEffect = hitMaster.hitEffects;
            AudioClip customHitAudio = hitMaster.hitAudio;

            if (customHitEffect != null)
            {
                GameObject go = Instantiate(customHitEffect, hitPosition.point, quatAngle);

                //To destroy particles if any.
                if (go.GetComponent<ParticleSystem>() != null)
                {
                    go.AddComponent<ParticleSystemDestroyer>();
                    go.GetComponent<ParticleSystemDestroyer>().maxDuration = hitMaster.heWaitTime;
                }

                go.SetActive(true);
            }

            if (customHitAudio != null)  AudioSource.PlayClipAtPoint(customHitAudio, hitPosition.point, hitMaster.hitVolume);
        }
	}
}
