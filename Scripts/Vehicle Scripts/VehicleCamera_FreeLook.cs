using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class VehicleCamera_FreeLook : MonoBehaviour
    {

        public Vector2 rotationRange = new Vector2(70, 361);
        public float rotationSpeed = 10f;

        private VehicleCamera_Master vehicleCameraMaster;
        private Transform targetTransform;
        private Vector3 targetAngles;
        private Quaternion capturedRotation;
        private bool isInFreelook;
        private float inputH;
        private float inputV;

        private void OnEnable()
        {
            SetInitialReferences();
            vehicleCameraMaster.EventAssignCameraTarget += AssignTarget;
        }

        private void OnDisable()
        {
            vehicleCameraMaster.EventAssignCameraTarget -= AssignTarget;
        }

        // Update is called once per frame
        void Update()
        {
            FreeLookRotation();
        }

        private void FixedUpdate()
        {
            FollowTargetRotation();
        }

        void SetInitialReferences()
        {
            vehicleCameraMaster = GetComponent<VehicleCamera_Master>();
        }

        void AssignTarget(Transform targ)
        {
            targetTransform = targ;
        }

        void FollowTargetRotation()
        {
            if (targetTransform != null && !isInFreelook) 
            {
                capturedRotation = Quaternion.LookRotation(targetTransform.forward, Vector3.up);

                transform.rotation = Quaternion.Lerp(transform.rotation, capturedRotation, 5 * Time.deltaTime);
                targetAngles = capturedRotation.eulerAngles;
            }
        }

        void FreeLookRotation()
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
            {
                if (targetTransform == null)
                {
                    return;
                }

                isInFreelook = true;
                transform.rotation = capturedRotation;

                inputH = Input.GetAxis("Mouse X");
                inputV = Input.GetAxis("Mouse Y");

                if (targetAngles.y > 180) targetAngles.y -= 360;
                if (targetAngles.x > 180) targetAngles.x -= 360;
                if (targetAngles.y < -180) targetAngles.y += 360;
                if (targetAngles.x < -180) targetAngles.x += 360;

                targetAngles.y += inputH * rotationSpeed;
                targetAngles.x += inputV * rotationSpeed;

                targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
                targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);

                transform.rotation = Quaternion.Euler(-targetAngles.x, targetAngles.y, 0);
            }

            else
            {
                isInFreelook = false;
            }
        }
    }
}

