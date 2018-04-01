﻿using UnityEngine;
using System.Collections;

namespace BaseFramework
{
	public class Destructible_TakeDamage : MonoBehaviour 
	{
        private Destructible_Master destructibleMaster;

		void Start()
		{
            SetInitialReferences();
        }

		void SetInitialReferences()
		{
            destructibleMaster = GetComponent<Destructible_Master>();
		}

        public void ProcessDamage(int damage)
        {
            if (destructibleMaster != null)
            destructibleMaster.CallEventDeductHealth(damage);
        }
		
	}
}

