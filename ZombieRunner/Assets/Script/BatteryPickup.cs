using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpBatteryInPocket(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void PickUpBatteryInPocket(GameObject playerObject)
    {
        FlashLightSystem flashLightSystem = playerObject.GetComponentInChildren<FlashLightSystem>();
        flashLightSystem.RestoreLightAngle(70);
        flashLightSystem.RestoreLightIntensity(7);
    }
}
