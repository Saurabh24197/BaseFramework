using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

namespace BaseFramework
{
    public class ScoreManager : MonoBehaviour
    {
       
        [Header("Attach Script to <Player_Root>.")]
		public Text scoreTextUI;

		[Space]
        public int totalScore;
        public int currentScore;
        public int enemyCount = 0;

        public string setIntStr = "CurrentScore";

        [Header("To get High Scores / Use for Testing Scenes")]
        public bool isTesting;
        public bool isMainMenu;

        private void Awake()
        {
            
            if (isTesting)
            {
                this.enabled = false;
                return;
            }

            if (!isMainMenu)
            {
                GetScores();
            }

            else GetHighScores();
            
        }

        // Update is called once per frame
        void Update()
        {
			if (currentScore == totalScore) {
				SaveGame ();
				StartCoroutine (ShowMessage ());
			} 

			else 
			{
				if (currentScore % 100 == 0)
				{
					scoreTextUI.text = "" + " Score: " + currentScore;
				}

				else scoreTextUI.text = "" + currentScore + "/" + totalScore;
			}
		}

        public void SaveGame()
        {
            PlayerPrefs.SetInt(setIntStr, currentScore);
        }

        void GetScores()
        {
            currentScore = PlayerPrefs.GetInt(setIntStr, 0);
            totalScore += currentScore;
        }

        IEnumerator ShowMessage()
        {
            scoreTextUI.text = "Complete!";
            yield return new WaitForSeconds(3.5f);

            Scene currentLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentLevel.buildIndex);
        }

        #region Used in Main Menu to get High Scores

        void GetHighScores()
        {
            scoreTextUI.text = "High Score = " + PlayerPrefs.GetInt(setIntStr, 0);
            this.enabled = false;
        }

        public void ResetHighScore()
        {
            PlayerPrefs.SetInt(setIntStr, 0);
            scoreTextUI.text = "High Score = " + PlayerPrefs.GetInt(setIntStr, 0);
        }

        #endregion
    }
}

