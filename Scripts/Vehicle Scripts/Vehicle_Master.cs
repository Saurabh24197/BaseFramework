using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class Vehicle_Master : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventExitVehicle;

        public delegate void VehicleEventHandler(GameObject driverGO, string driverTag, LayerMask driverLayer);
        public event VehicleEventHandler EventEnterVehicle;

        public bool isVehicleOccupied;
        public string defaultTag = "Untagged";
        public string exitButton = "PickUpItem";

        public int defaultLayerNumber;
        public LayerMask defaultLayer;

        public Transform cabin;
        public GameObject vehicleCamera;
        public Transform cameraTarget;

        [HideInInspector]
        public GameObject driver;

        // Use this for initialization
        void Start()
        {
            ApplyDefaultLayer();
        }

        void ApplyDefaultLayer()
        {
            defaultLayerNumber = gameObject.layer;
        }

        public void CallEventExitVehicle()
        {
            if (EventExitVehicle != null)
            {
                EventExitVehicle();
            }
        }

        public void CallEventEnterVehicle(GameObject driverGO, string dTag, LayerMask dLayer)
        {
            if (EventEnterVehicle != null)
            {
                EventEnterVehicle(driverGO, dTag, dLayer);
            }
        }
    }
}

