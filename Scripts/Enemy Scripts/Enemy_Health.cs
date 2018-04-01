using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Enemy_Health : MonoBehaviour 
    {

        private Enemy_Master enemyMaster;
        public  int enemyHealth;
        public float healthLow = 25;
        
        void OnEnable()
        {
            SetInitialReferences();

            enemyMaster.EventEnemyDeductHealth += DeductHealth;
            enemyMaster.EventEnemyIncreaseHealth += IncreaseHealth;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDeductHealth -= DeductHealth;
            enemyMaster.EventEnemyIncreaseHealth -= IncreaseHealth;
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Period))
            {
                enemyMaster.CallEventEnemyIncreaseHealth(30);
            }
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
        }

        void DeductHealth(int healthChange)
        {
            enemyHealth -= healthChange;

            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                enemyMaster.CallEventEnemyDie();

                if (Random.Range(0,5) <= 2)
                Destroy(gameObject, Random.Range(8, 15));
                    
            }

            CheckHealthFraction();
        }

        void CheckHealthFraction()
        {
            if (enemyHealth <= healthLow && enemyHealth > 0)
            {
                enemyMaster.CallEventEnemyHealthLow();
            }
            else if (enemyHealth > healthLow)
            {
                enemyMaster.CallEventEnemyHealthRecovered();
            }
        }

        void IncreaseHealth(int healthChange)
        {
            enemyHealth += healthChange;
            if (enemyHealth > 100)
            {
                enemyHealth = 100;
            }

            CheckHealthFraction();
        }
    }
}
