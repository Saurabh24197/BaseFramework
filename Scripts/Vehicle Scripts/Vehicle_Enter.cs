using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Vehicles.Car;
using UnityEngine.AI;

namespace BaseFramework
{
    public class Vehicle_Enter : MonoBehaviour
    {
        private Vehicle_Master vehicleMaster;

        private void OnEnable()
        {
            SetInitialReferences();
            vehicleMaster.EventEnterVehicle += EnterVehicle;
        }

        private void OnDisable()
        {
            vehicleMaster.EventEnterVehicle -= EnterVehicle;
        }

        void SetInitialReferences()
        {
            vehicleMaster = GetComponent<Vehicle_Master>();
        }

        void EnterVehicle(GameObject driver, string driverTag, LayerMask driverLayer)
        {
            SetCameraTarget();
            PlaceDriverInVehicle(driver);
            ActivateVehicleControlScript();
            TurnOffNavMeshObstacle();
            ApplyTagAndLayerToVehicle(driverTag, driverLayer);
            EnableVehiclExit();
        }

        void SetCameraTarget()
        {
            if (vehicleMaster.vehicleCamera == null || vehicleMaster.cameraTarget == null)
            {
                return;
            }

            vehicleMaster.vehicleCamera.transform.rotation = transform.rotation;
            vehicleMaster.vehicleCamera.SetActive(true);

            if (vehicleMaster.vehicleCamera.GetComponent<VehicleCamera_Master>() != null)
            {
                vehicleMaster.vehicleCamera.GetComponent<VehicleCamera_Master>().CallEventAssignCameraTarget(vehicleMaster.cameraTarget);
            }
        }

        void ActivateVehicleControlScript()
        {
            if (GetComponent<CarUserControl>() != null)
            {
                GetComponent<CarUserControl>().enabled = true;
            }

            if (GetComponent<CarController>() != null)
            {
                GetComponent<CarController>().enabled = true;
            }
        }

        void TurnOffNavMeshObstacle()
        {
            if (GetComponent<NavMeshObstacle>() != null)
            {
                GetComponent<NavMeshObstacle>().enabled = false;
            }
        }

        void ApplyTagAndLayerToVehicle(string driverTag, LayerMask driverLayer)
        {
            gameObject.tag = driverTag;
            gameObject.layer = driverLayer;
        }

        void EnableVehiclExit()
        {
            if (GetComponent<Vehicle_Exit>() != null)
            {
                GetComponent<Vehicle_Exit>().enabled = true;
            }
        }

        void PlaceDriverInVehicle(GameObject driver)
        {
            driver.SetActive(false);

            if (vehicleMaster.cabin == null)
            {
                Debug.LogWarning("Assign Cabin transform to VehicleMaster");
                return;
            }

            driver.transform.SetParent(vehicleMaster.cabin);
            vehicleMaster.driver = driver;
            vehicleMaster.isVehicleOccupied = true;
        }
    }
}

