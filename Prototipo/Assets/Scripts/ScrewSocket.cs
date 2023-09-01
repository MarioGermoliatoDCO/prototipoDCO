using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewSocket : MonoBehaviour
{
    XRSocketInteractor screwSocket;

    private void Start()
    {
        screwSocket = GetComponent<XRSocketInteractor>();
    }

    public void CheckScrewPlaced()
    {
        IXRSelectInteractable screw = screwSocket.GetOldestInteractableSelected();

        Screw screwPlaced;
       if (screw.transform.TryGetComponent<Screw>(out screwPlaced))
        {
            screwPlaced.ChangeIsPlacedAgain();
        }
    }
}
