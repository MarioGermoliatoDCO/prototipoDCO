using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FilterCase : MonoBehaviour
{
    [SerializeField] private Screw[] ScrewsArray;

    private XRGrabInteractable grabInteractable;    

    private void Start()
    {
        Screw.OnScrewRemoved += Screw_OnScrewRemoved;
        grabInteractable = GetComponent<XRGrabInteractable>();
    }  

    private void Screw_OnScrewRemoved(object sender, System.EventArgs e)
    {
        int numberOfScrewsRemoved = 0;
        for (int i = 0; i < ScrewsArray.Length; i++)
        {
            if (ScrewsArray[i].GetScrewRemoved())
            {
                numberOfScrewsRemoved++;
            }
        }

        if (numberOfScrewsRemoved == ScrewsArray.Length)
        {
            grabInteractable.enabled = true;
        }
    }
}
