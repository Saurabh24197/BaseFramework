using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BaseFramework
{

    public class Player_Health : MonoBehaviour
    {

        private GameManager_Master gameManagerMaster;
        private Player_Master playerMaster;

        public int playerHealth;
        public int maxPlayerHealth = 100;
        public Text healthText;

        public Image healthBarContentImage;
        private float healthBarFillAmt = 1;
        


        void OnEnable()
        {
            SetInitialReferences();
            SetUI();

            playerMaster.EventPlayerHealthDeduction += DeductHealth;
            playerMaster.EventPlayerHealthIncrease += IncreaseHealth;
	
        }

        void OnDisable()
        {
            playerMaster.EventPlayerHealthDeduction -= DeductHealth;
            playerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
            playerMaster = GetComponent<Player_Master>();
        }

        void DeductHealth(int healthChange)
        {
            playerHealth -= healthChange;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                gameManagerMaster.CallEventGameOver();
            }

            SetUI();
        }

        void IncreaseHealth(int healthChange)
        {
            playerHealth += healthChange;

            if (playerHealth > maxPlayerHealth)
            {
                playerHealth = maxPlayerHealth;
            }

            SetUI();
        }

        void SetUI()
        {
            if ( healthText != null)
            {
                healthText.text = playerHealth.ToString();
            }

            if (healthBarContentImage != null)
            {
                healthBarFillAmt = HealthMapper(playerHealth, maxPlayerHealth);
				StartCoroutine (SetFill (healthBarFillAmt));
            }
        }

		IEnumerator SetFill(float healthBarFillAmt)
		{
			float alpha = 0.2f;
			float currentFill = healthBarContentImage.fillAmount;

			do 
			{
				if (healthBarFillAmt == 0)
				{
					healthBarContentImage.fillAmount = 0;
					yield break;
				}

				float lerpVal = Mathf.Lerp (currentFill, healthBarFillAmt, alpha);
				alpha += 0.2f;
				lerpVal = (alpha == 1) ? healthBarFillAmt : lerpVal;
				healthBarContentImage.fillAmount = lerpVal; 

				yield return new WaitForSeconds (0.03f);

			} while (alpha != 1) ;
		}

        float HealthMapper(float value, float max)
        {
			//This function was big.
            return (value / max);
        }
    }
}

