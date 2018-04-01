using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_Name : MonoBehaviour 
    {
        public string itemName;

        
        void Start()
        {
            SetItemName();
        }

        void SetItemName()
        {
            if (itemName != null)
            {
                transform.name = itemName;
            }

            else itemName = transform.name;
            
        }
    }
}
