using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BaseFramework
{
    public class Player_Inventory : MonoBehaviour
    {

        public Transform inventoryPlayerParent;
        public Transform inventoryUIParent;
        public GameObject uiButton;

        private Player_Master playerMaster;
        private GameManager_ToggleInventoryUi inventoryUIScript;
        private float timeToPlaceInHands = 0.1f;
        private Transform currentlyHeldItem;
        private int counter;
        private string buttonText;

        [SerializeField]
        private List<Transform> listInventory = new List<Transform>();


        void OnEnable()
        {
            SetInitialReferences();
            DeactivateAllInventoryItems();
            UpdateInventoryListAndUI();
            CheckIfHandsEmpty();


            playerMaster.EventInventoryChanged += UpdateInventoryListAndUI;
            playerMaster.EventInventoryChanged += CheckIfHandsEmpty;
            playerMaster.EventHandsEmpty += ClearHands;
        }

        void OnDisable()
        {
            playerMaster.EventInventoryChanged -= UpdateInventoryListAndUI;
            playerMaster.EventInventoryChanged -= CheckIfHandsEmpty;
            playerMaster.EventHandsEmpty -= ClearHands;
            ClearHands();
        }

        void SetInitialReferences()
        {
            inventoryUIScript = GameObject.Find("GameManager").GetComponent<GameManager_ToggleInventoryUi>();
            playerMaster = GetComponent<Player_Master>();
             
        }

        void UpdateInventoryListAndUI()
        {
            counter = 0;

            listInventory.Clear();
            listInventory.TrimExcess();

            ClearInventoryUI();

            foreach (Transform child in inventoryPlayerParent)
            {
                if (child.CompareTag("Item"))
                {
                    listInventory.Add(child);

                    GameObject go = Instantiate(uiButton) as GameObject;
                    buttonText = child.name;
                    go.GetComponentInChildren<Text>().text = buttonText;

                    int index = counter;
                    go.GetComponent<Button>().onClick.AddListener(delegate 
                    {
                        ActivateInventoryItem(index);
                    });

                    go.GetComponent<Button>().onClick.AddListener(inventoryUIScript.ToggleInventoryUI);
                    go.transform.SetParent(inventoryUIParent, false);
                    counter++;
                }
            }
        }

        void CheckIfHandsEmpty()
        {
            if ( currentlyHeldItem == null && listInventory.Count > 0)
            {
                StartCoroutine(PlaceItemsInHands(listInventory[0]));
            } 
        }
        
        void ClearHands()
        {
            currentlyHeldItem = null;
        }

        void ClearInventoryUI()
        {

            foreach (Transform child in inventoryUIParent)
            {
                Destroy(child.gameObject);
            }
        }

        public void ActivateInventoryItem(int inventoryIndex)
        {
            DeactivateAllInventoryItems();
            StartCoroutine(PlaceItemsInHands(listInventory[inventoryIndex]) );
        }

        void DeactivateAllInventoryItems()
        {
            foreach(Transform child in inventoryPlayerParent)
            {
                if ( child.CompareTag("Item"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        IEnumerator PlaceItemsInHands( Transform itemTransform)
        {
            yield return new WaitForSeconds(timeToPlaceInHands);
            currentlyHeldItem = itemTransform;
            currentlyHeldItem.gameObject.SetActive(true);
        }

    }
}


