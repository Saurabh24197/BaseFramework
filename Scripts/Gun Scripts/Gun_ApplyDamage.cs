using UnityEngine;
using System.Collections;

namespace BaseFramework
{

	public class Gun_ApplyDamage : MonoBehaviour 
	{

        private Gun_Master gunMaster;
        public int damage = 10;
        public int npcGunDamage = 10;

	    
		void OnEnable()
		{
            SetInitialReferences();

            gunMaster.EventShotEnemy += ApplyDamage;
            gunMaster.EventShotDefault += ApplyDamage;
		}

		void OnDisable()
		{
            gunMaster.EventShotEnemy -= ApplyDamage;
            gunMaster.EventShotDefault -= ApplyDamage;
        }

		void SetInitialReferences()
		{
            gunMaster = GetComponent<Gun_Master>();
		}
        
		void ApplyDamage(RaycastHit hitPosition, Transform hitTransform)
        {

            if (transform.root.tag.Equals("Player"))
            {
                hitTransform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);

                hitTransform.SendMessage("CallEventPlayerHealthDeduction", damage, SendMessageOptions.DontRequireReceiver);
                hitTransform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
            }

            else
            {
                hitTransform.SendMessage("ProcessDamage", npcGunDamage, SendMessageOptions.DontRequireReceiver);

                hitTransform.SendMessage("CallEventPlayerHealthDeduction", npcGunDamage, SendMessageOptions.DontRequireReceiver);
                hitTransform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
            }

        }
	}
}
