using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class TriggerExample : MonoBehaviour
    {
       
        private GameManager_EventMaster eventMasterScript;
        public GameObject lightings;
        

        private Color intenseColor = new Color(0, 0.7f, 1f);
        public GameObject audioBegin;
        public GameObject EnemyAudio;

        public GameObject player;
        
       

        void Start()
        {
            SetInitialReferences();

        }
        void OnTriggerEnter (Collider Other)
        {
            eventMasterScript.CallMyGeneralEvent();
            player.GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y + 20, player.transform.position.z - 30);
            player.GetComponent<Transform>().rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y + 270, player.transform.rotation.z, 0);
            Destroy(gameObject);
            SetLightandAudioWhenEnemySpawns();
            
        }

        void SetInitialReferences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();          
        }

        void SetLightandAudioWhenEnemySpawns()
        {
            lightings.GetComponent<Light>().color = intenseColor;
            lightings.GetComponent<Light>().intensity = 2f;

            audioBegin.GetComponent<AudioSource>().Pause();
            //audioBegin.SetActive(false);
            
            EnemyAudio.SetActive(true);
        }
    }
}

