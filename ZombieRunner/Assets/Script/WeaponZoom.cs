using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] Camera fpsCamera;    
    [SerializeField]RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;

    [SerializeField] float zoomedInFOV = 20f;

    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.5f;



    private bool zoomedInToggle = false;
    public bool ZoomFlg
    {
        get
        {
            return zoomedInToggle;
        }
    }

    // private void Start()
    // {
    //     fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
    // }
    private void Update()
    {
        //Pressed secondary button.
        if(Input.GetMouseButtonDown(1))
        {
            if(zoomedInToggle == false)
            {
                ZoomIn();
            }

            else
            {
                ZoomOut();
            }
        }
    }

    public void ZoomOut()
    {
        zoomedInToggle = false;

        fpsCamera.fieldOfView = zoomedOutFOV;
        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
    }

    public void ZoomIn()
    {
        zoomedInToggle = true;

        fpsCamera.fieldOfView = zoomedInFOV;
        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;
    }
}
