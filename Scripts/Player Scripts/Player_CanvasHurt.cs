 using UnityEngine;
using System.Collections;

namespace BaseFramework
{

    public class Player_CanvasHurt : MonoBehaviour 
    {

        public GameObject hurtCanvas;
        private Player_Master playerMaster;
        private float secondsTillHide = 2f;

        void OnEnable()
        {
            SetInitialReferences();
            HideHurtCanvase();
            playerMaster.EventPlayerHealthDeduction += TurnOnHurtEffect;
        }

        void OnDisable()
        {
            playerMaster.EventPlayerHealthDeduction -= TurnOnHurtEffect;
        }

        void SetInitialReferences()
        {
            playerMaster = GetComponent<Player_Master>();
        }

        void TurnOnHurtEffect(int damage)
        {
            if (damage > 0)
            {
                if (hurtCanvas != null)
                {
                    StopAllCoroutines();
                    hurtCanvas.SetActive(true);
                    StartCoroutine(ResetHurtCanvas());
                }
            }

        }

        IEnumerator ResetHurtCanvas()
        {
            yield return new WaitForSeconds(secondsTillHide);
            HideHurtCanvase();
        }


        void HideHurtCanvase()
        {
            hurtCanvas.SetActive(false);
        }
    }
}

