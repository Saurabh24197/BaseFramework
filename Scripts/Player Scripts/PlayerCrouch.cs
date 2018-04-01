using UnityEngine;
using System.Collections;

namespace BaseFramework
{
	public class PlayerCrouch : MonoBehaviour 
	{

        public float crouchSpeed;
        CharacterController charController;
        public Transform player;
        private float charHeight;
        private Vector3 pos;

        void Start()
		{
            player = transform;
            charController = GetComponent<CharacterController>();
            charHeight = charController.height;
		}

		void LateUpdate()
		{
            float crouchHeight = charHeight;

            if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
            {
                crouchHeight = 1;
            }

            float lastHeight = charController.height;
            charController.height = Mathf.Lerp(charController.height, crouchHeight, Time.deltaTime * 5);

            pos.x = player.position.x;
            pos.z = player.position.z;

            pos.y = player.position.y + (charController.height - lastHeight) / 2;
            player.position = pos;
            
		}	
	}
}

