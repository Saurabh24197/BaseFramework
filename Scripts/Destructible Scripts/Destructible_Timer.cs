using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Destructible_Timer : MonoBehaviour
    {
        private Destructible_Master destructibleMaster;
        private Item_Master itemMaster;

        [TextArea]
        public string scriptInfo = "Used to Perform a Timer based Explosion." +
            "\nRemoveComponent(<Destructible_CollisionDetection>" +
            "\n & <Destructible_Health>" +
            "\n & <Destructible_Degenerate>";
        public float waitTime = 3.0f;

        private void OnEnable()
        {
            SetInitialReferences();

            if (itemMaster != null)
                itemMaster.EventObjectThrow += DestroyUsingTimer;
        }

        private void OnDisable()
        {
            if (itemMaster != null)
                itemMaster.EventObjectThrow -= DestroyUsingTimer;
        }

        void SetInitialReferences()
        {
            if (GetComponent<Item_Master>() != null)
            {
                itemMaster = GetComponent<Item_Master>();
            }

            destructibleMaster = GetComponent<Destructible_Master>();
        }

        void DestroyUsingTimer()
        {
            //new WaitForSeconds(waitTime);
            //This sure is a Hacky business, atleast it works

            if (GetComponent<Item_Pickup>() != null)
            {
                GetComponent<Item_Pickup>().enabled = false;
            }

            StartCoroutine(BeginExplosionTimer());
        }

        IEnumerator BeginExplosionTimer()
        {
            yield return new WaitForSeconds(waitTime);
            destructibleMaster.CallEventDestroyMe();
        }
    }
}

