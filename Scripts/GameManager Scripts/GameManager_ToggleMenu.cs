using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BaseFramework
{
    public class GameManager_ToggleMenu : MonoBehaviour
    {

        private GameManager_Master gameManagerMaster;
        public GameObject menu;

        // Use this for initialization
        void Start()
        {
            ToggleMenu();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForMenuToggleRequest();
        }

        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.GameOverEvent += ToggleMenu;
            
        }

        void OnDisable()
        {
            gameManagerMaster.GameOverEvent -= ToggleMenu;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();      
        }

        void CheckForMenuToggleRequest()
        {
            if ( Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn)
            {
                ToggleMenu();
            }

        }

        public void ToggleMenu()
        {
            if (menu != null)
            {
                gameManagerMaster.ToggleUIGameObjects(menu.activeSelf);

                menu.SetActive(!menu.activeSelf);
                gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;

                gameManagerMaster.CallEventMenuToggle();
            }

            else
            {
                Debug.LogWarning("You need to assign a UI gameObject to the Toggle Script in te Inspector.");
            }
        }
    }
}
