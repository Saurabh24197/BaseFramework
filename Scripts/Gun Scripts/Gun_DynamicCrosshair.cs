using UnityEngine;
using System.Collections;

namespace BaseFramework
{

    public class Gun_DynamicCrosshair : MonoBehaviour
    {

        private Gun_Master gunMaster;

        public Transform canvasDynamicCrosshair;
        private Transform playerTransform;
        private Transform weaponCamera;

        private float playerSpeed;
        private float captureTime;
        private float nextCaptureTime;
        private float captureInterval = 0.5f;

        private Vector3 lastPosition;
        public Animator crosshairAnimator;
        public string weaponCameraName;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            CapturePlayerSpeed();
            ApplySpeedToAnimation();
        }

        void SetInitialReferences()
        {
            gunMaster = GetComponent<Gun_Master>();
            playerTransform = GameManager_References._player.transform;
            FindWeaponCamera(playerTransform);

            SetCameraOnDynamicCrosshairCanvas();
            SetCameraOnDynamicCrosshairCanvas();
        }

        void CapturePlayerSpeed()
        {
            if (Time.time > nextCaptureTime)
            {
                nextCaptureTime = Time.time + captureInterval;
                playerSpeed = (playerTransform.position - lastPosition).magnitude / captureInterval;
                lastPosition = playerTransform.position;

                gunMaster.CallEventSpeedCaptured(playerSpeed);
            }
        }

        void ApplySpeedToAnimation()
        {
            if (crosshairAnimator != null)
            {
                crosshairAnimator.SetFloat("Speed", playerSpeed);
            }
        }

        void FindWeaponCamera(Transform transformToSearchThrough)
        {
            if (transformToSearchThrough != null)
            {
                if(transformToSearchThrough.name == weaponCameraName)
                {
                    weaponCamera = transformToSearchThrough;
                    return;
                }

                foreach (Transform child in transformToSearchThrough)
                {
                    FindWeaponCamera(child);
                }
            }
        }

        void SetCameraOnDynamicCrosshairCanvas()
        {
            if (canvasDynamicCrosshair != null && weaponCamera != null)
            {
                canvasDynamicCrosshair.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                canvasDynamicCrosshair.GetComponent<Canvas>().worldCamera = weaponCamera.GetComponent<Camera>();
            }
        }

        void SetPlaneDistanceOnDynamicCrosshairCanvas()
        {
            if (canvasDynamicCrosshair != null)
            {
                canvasDynamicCrosshair.GetComponent<Canvas>().planeDistance = 1;
            }
        }
	}
}
