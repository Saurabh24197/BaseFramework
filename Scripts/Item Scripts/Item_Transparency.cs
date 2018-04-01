using UnityEngine;
using System.Collections;

namespace BaseFramework
{
	public class Item_Transparency : MonoBehaviour 
	{
        private Item_Master itemMaster;
        public Material transparentMat;
        private Material primaryMat;

        public GameObject[] leafNodes;

		void OnEnable()
		{
            SetInitialReferences();

            itemMaster.EventObjectPickup += SetToTransparentMaterial;
            itemMaster.EventObjectThrow += SetToPrimaryMaterial;
		}

		void OnDisable()
		{
            itemMaster.EventObjectPickup -= SetToTransparentMaterial;
            itemMaster.EventObjectThrow -= SetToPrimaryMaterial;
        }

		void Start()
		{
            CaptureStartingMaterial();

            if (gameObject.transform.root.tag == GameManager_References._playerTag)
            {
                SetToTransparentMaterial();
            }
        }

		void SetInitialReferences()
		{
            itemMaster = GetComponent<Item_Master>();
            
		}

		void CaptureStartingMaterial()
        {
            primaryMat = GetComponent<Renderer>().material;
        }

        void SetToPrimaryMaterial()
        {
            GetComponent<Renderer>().material = primaryMat;

            foreach (GameObject go in leafNodes)
            {
                go.GetComponent<Renderer>().material = primaryMat;
            }
        }

        void SetToTransparentMaterial()
        {
            GetComponent<Renderer>().material = transparentMat;

            foreach(GameObject go in leafNodes)
            {
                go.GetComponent<Renderer>().material = transparentMat;
            }
        }
	}
}

