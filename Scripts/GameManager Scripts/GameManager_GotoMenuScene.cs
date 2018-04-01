using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace BaseFramework
{
    public class GameManager_GotoMenuScene : MonoBehaviour
    {

        private GameManager_Master gameManagerMaster;
        
        void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.GoToMenuSceneEvent += GotoMenuScene;
        }

        void OnDisable()
        {
            gameManagerMaster.GoToMenuSceneEvent -= GotoMenuScene;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void GotoMenuScene()
        {
            //Application.LoadLevel(0);
            SceneManager.LoadScene(0);
        }
    }
}

