using UnityEngine;
using System.Collections;

namespace BaseFramework
{
    public class GameManager_ToggleInventoryUi : MonoBehaviour
    {

        [Tooltip("Does this GameMode have a Inventory? Set to true if it is the case.")]
        public bool hasInventory;
        public GameObject inventoryUI;
        public string toggleInventoryButton;

        private GameManager_Master gameManagerMaster;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences(); 
        }

        // Update is called once per frame
        void Update()
        {
            CheckForInventoryUIToggleRequest();
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();

            if (toggleInventoryButton == "")
            {
                Debug.LogWarning("Type in the name of Button used to Toggle the Inventory in "
                    +"GameManager_ToggleInventoryUI");
                this.enabled = false;
            }
        }
        
        void CheckForInventoryUIToggleRequest()
        {
            if ( Input.GetButtonUp(toggleInventoryButton) && !gameManagerMaster.isMenuOn
                && !gameManagerMaster.isGameOver && hasInventory
                && GameManager_References._player.activeSelf)
            {
                ToggleInventoryUI();
            }
        }

        public void ToggleInventoryUI()
        {
            if (inventoryUI != null)
            {
                gameManagerMaster.ToggleUIGameObjects(inventoryUI.activeSelf);

                inventoryUI.SetActive(!inventoryUI.activeSelf);
                gameManagerMaster.isInventoryUIOn = !gameManagerMaster.isInventoryUIOn;
                gameManagerMaster.CallEventInventoryUIToggle(); 
            }
        }
    }
}

