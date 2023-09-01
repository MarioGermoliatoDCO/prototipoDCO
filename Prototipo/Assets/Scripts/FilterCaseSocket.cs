using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FilterCaseSocket : XRSocketInteractor
{
    [SerializeField] XRSocketInteractor[] screwsSocketsArray;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);        

        foreach (XRSocketInteractor socket in screwsSocketsArray)
        {
            socket.enabled = true;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);        
        foreach (XRSocketInteractor socket in screwsSocketsArray)
        {
            socket.enabled = false;
        }
    }
}
