using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] XRInteractorLineVisual leftTeleportation;
    [SerializeField] XRInteractorLineVisual rightTeleportation;

    [SerializeField] InputActionProperty leftActivate;
    //[SerializeField] InputActionProperty rightActivate;
        

    private void Update()
    {
        leftTeleportation.enabled = (leftActivate.action.ReadValue<float>() > 0.1f);
        //rightTeleportation.enabled = (rightActivate.action.ReadValue<float>() > 0.1f);        
    }
}
