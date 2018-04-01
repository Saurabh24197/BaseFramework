using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class VehicleCamera_Follow : MonoBehaviour
    {
        private VehicleCamera_Master vehicleCameraMaster;
        private Transform targetTransform;

        private void OnEnable()
        {
            SetInitialReferences();
            vehicleCameraMaster.EventAssignCameraTarget += AssignTarget;
        }

        private void OnDisable()
        {
            vehicleCameraMaster.EventAssignCameraTarget -= AssignTarget;
        }

        void FixedUpdate()
        {
            StayWithTarget();
        }

        void SetInitialReferences()
        {
            vehicleCameraMaster = GetComponent<VehicleCamera_Master>();
        }

        void AssignTarget(Transform targ)
        {
            targetTransform = targ;
        }

        void StayWithTarget()
        {
            if (targetTransform == null)
            {
                return;
            }

            transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * 5);
        }
    }
}


