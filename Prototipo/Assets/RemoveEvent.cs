using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
// using UnityEditor.Events;

public class RemoveEvent : MonoBehaviour
{
    public UnityEvent[] selectEnterEvents;
    private int selectEnterEventsCalled;
    public UnityEvent[] selectExitEvents;
    private int selectExitEventsCalled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveEventObj(SelectEnterEventArgs args){
        if (selectEnterEventsCalled < selectEnterEvents.Length)
        {
            selectEnterEvents[selectEnterEventsCalled].Invoke();
            selectEnterEventsCalled++;
        }
    }

    public void RemoveExitEventObj(SelectExitEventArgs args){
        if (selectExitEventsCalled < selectExitEvents.Length)
        {
            selectExitEvents[selectExitEventsCalled].Invoke();
            selectExitEventsCalled++;
        }
    }
}
