using UnityEngine;
using System.Collections;

namespace BaseFramework
{
    public class GameManager_ToggleCurosor : MonoBehaviour
    {
        private GameManager_Master gameManagerMaster;
        private bool isCursorLocked = true;

        void OnEnable()
        {

            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += ToggleCursorState;
            gameManagerMaster.InventoryUIToggleEvent += ToggleCursorState;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
            gameManagerMaster.InventoryUIToggleEvent -= ToggleCursorState;
        }


        // Update is called once per frame
        void Update()
        {
            CheckIfCursorShouldBeLocked();
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void ToggleCursorState()
        {
            isCursorLocked = !isCursorLocked;
        }

        void CheckIfCursorShouldBeLocked()
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;      
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

    }
}

