using UnityEngine;
using System.Collections;

namespace BaseFramework
{
    public class GameManager_TogglePause : MonoBehaviour
    {
        //A Reference to GameManager_Master Script
        private GameManager_Master gameManagerMaster;
        private bool isPaused;

        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += TogglePause;
            gameManagerMaster.InventoryUIToggleEvent += TogglePause;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= TogglePause;
            gameManagerMaster.InventoryUIToggleEvent -= TogglePause;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TogglePause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }


    }
}

