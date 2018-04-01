using UnityEngine;
using System.Collections;


namespace BaseFramework
{
    public class Item_Tag : MonoBehaviour 
    {
        public string itemTag = "Item";

        void OnEnable()
        {
            SetTag();
        }

        void SetTag()
        {
            if ( itemTag == "")
            {
                itemTag = "Untagged";
            }

            transform.tag = itemTag;
        }
    }
}
