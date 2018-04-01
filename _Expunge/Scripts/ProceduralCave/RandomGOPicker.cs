using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class RandomGOPicker : MonoBehaviour
    {
        public bool destroyGO = false;
        public List<GameObject> goList;


        void OnEnable()
        {
            int i = Random.Range(0, goList.Count);

            if (goList[i] != null)
            {
                Instantiate(goList[i], transform.position, transform.rotation);
            }

            if (destroyGO) Destroy(gameObject);
        }
    }
}

