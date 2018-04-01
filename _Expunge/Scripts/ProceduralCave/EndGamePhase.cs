using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class EndGamePhase : MonoBehaviour
    {

        public int maxScoreCount;
        public ScoreManager scoreInstance;

        [Header("List of Objects to Set True on Enter")]
        public List<GameObject> goTrue;
        [Header("List of Objects to Set True on Enter & False on Exit")]
        public List<GameObject> goTrueFalse;

        private void Start()
        {
            if (scoreInstance.currentScore < maxScoreCount)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == GameManager_References._playerTag)
            {
                if (scoreInstance.currentScore > maxScoreCount)
                {
                    foreach (GameObject go in goTrue)
                    {
                        go.SetActive(true);
                    }

                    foreach (GameObject go in goTrueFalse)
                    {
                        go.SetActive(true);
                    }

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == GameManager_References._playerTag)// & uiGameComplete.activeSelf)
            {
                foreach (GameObject go in goTrueFalse)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}

