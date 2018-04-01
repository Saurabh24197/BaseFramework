using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class VehicleCamera_Master : MonoBehaviour
    {
        public delegate void CameraTargetEventHandler(Transform targetTransform);
        public event CameraTargetEventHandler EventAssignCameraTarget;

        public void CallEventAssignCameraTarget (Transform targTransform)
        {
            if (EventAssignCameraTarget != null)
            {
                EventAssignCameraTarget(targTransform);
            }
        }
    }
}
