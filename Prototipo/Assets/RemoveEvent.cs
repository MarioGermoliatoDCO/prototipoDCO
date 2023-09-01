using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor.Events;

public class RemoveEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveEventObj(SelectEnterEventArgs args){
        // Debug.Log(args.interactableObject.selectEntered.GetPersistentEventCount());
        for(int i = 0; i < args.interactableObject.selectEntered.GetPersistentEventCount()  ; i++){
            args.interactableObject.selectEntered.RemoveAllListeners();
            UnityEventTools.RemovePersistentListener(args.interactableObject.selectEntered, i);
        }
    }
}
