using UnityEngine;
using System.Collections;

namespace BaseFramework
{
    public class GameManager_References : MonoBehaviour
    {

        public string playerTag;
        //For the Script purpose
        public static string _playerTag;

        public string enemyTag;
        public static string _enemyTag;

        public GameObject playerGO;
        public static GameObject _player;

        void OnEnable()
        {
            if (playerTag == "")
            {
                Debug.LogWarning("Please type in the name of playerTag in GameManager_References slot in the Inspector" +
                    "or else the this system will not work.");
            }

            if (enemyTag == "")
            {
                Debug.LogWarning("Please type in the name of enemyTag in GameManager_References slot in the Inspector" +
                    "or else the this system will not work.");
            }

            _playerTag = playerTag;
            _enemyTag = enemyTag;

            //_player = GameObject.FindGameObjectWithTag(_playerTag);
            _player = playerGO;
            
        }
    }
}

