using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Chapter1
{


    public class Welcome : MonoBehaviour
    {

        public string greenredMessage;
        public Text textWelcome;

        public GameObject canvasWelcome;
        //private float waitTime1 = 10f;


        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
            MyWelcomeMessage();
        }


        void SetInitialReferences()
        {
            textWelcome = GameObject.Find("TextWelcome").GetComponent<Text>();
            greenredMessage = "\n[Welcome to sysOxygen]\n[System reboot is in progress]\n\nA cache of DataCoins awaits you.\nGrab the DataCoins before the reboot.\nLocate the nearest Marker to proceed.\nHit begin to load EntryHack.";

        }
        void MyWelcomeMessage()
        {
            if (textWelcome != null)
            {
                textWelcome.text = greenredMessage;
            }

            //StartCoroutine(DisableCanvas(3.5f));
        }



        //IEnumerator DisableCanvas(float waitTime2)
        //{
        //    yield return new WaitForSeconds(waitTime2);
        //    canvasWelcome.SetActive(false);
            
        //}
    }
}


