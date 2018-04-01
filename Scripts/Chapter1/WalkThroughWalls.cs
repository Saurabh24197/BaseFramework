using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class WalkThroughWalls : MonoBehaviour
    {
        
        private Color myColor = new Color(0.5f, 1f, 0.5f, 0.3f);
        private GameManager_EventMaster eventMasterScript;
        public GameObject alertCanvas;
        private float waitTime = 7f;


        void OnEnable()
        {
            SetInitialReferences();
            eventMasterScript.myGeneralEvent += SetLayerToNotSolid;
        }

        void OnDisable()
        {
            eventMasterScript.myGeneralEvent -= SetLayerToNotSolid;
        }

        void SetLayerToNotSolid()
        {
            gameObject.layer = LayerMask.NameToLayer("Not Solid");
            GetComponent<Renderer>().material.SetColor("_Color", myColor);
            GetComponent<BoxCollider>().enabled = false;
            alertCanvas.SetActive(true);

            StartCoroutine(DisableAlertCanvas(waitTime));
            


        }

        void SetInitialReferences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
        }

        IEnumerator DisableAlertCanvas(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            alertCanvas.SetActive(false);

        }
    }
}

