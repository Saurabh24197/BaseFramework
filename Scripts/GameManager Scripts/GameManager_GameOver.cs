using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace BaseFramework
{
    public class GameManager_GameOver : MonoBehaviour
    {

        private GameManager_Master gameManagerMaster;
        public GameObject panelGameOver;
        public ScoreManager scoreManager;

		public Button btnContinue;


        void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.GameOverEvent += PerformGameOverOperations;
        }

        void OnDisable()
        {
            gameManagerMaster.GameOverEvent -= PerformGameOverOperations;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void PerformGameOverOperations()
        {
            if ( panelGameOver != null)
            {
                panelGameOver.SetActive(true);
            }

            if (scoreManager != null)
            {
                scoreManager.ResetHighScore();
            }

            if (btnContinue != null)
			{
				btnContinue.interactable = false;
			}


        }

    }
}

