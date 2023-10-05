using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// using UnityEditor.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class FilterGrab : XRGrabInteractable
{
    [SerializeField] private bool caseRemoved;
    [SerializeField] private Transform filterTransform;
    [SerializeField] private float filterLimitX = 1.6f;


    private const string DEFAULT_LAYER = "Default";
    private const string GRAB_LAYER = "Grab";

    private Transform parentTransform;
    private bool isGrabbed;
    private Vector3 limitPositions;
    private Vector3 limitDistances = new Vector3(0.2f, 0.4f, 0.4f);

    public List<UnityEvent> finishedPushing;
    public int finishedPushCalled;


    private void Start()
    {
        parentTransform = transform.parent.transform;
        limitPositions = filterTransform.localPosition;
    }

    private void Update()
    {
        if (isGrabbed && filterTransform != null)
        {
            filterTransform.localPosition = new Vector3(transform.localPosition.x, filterTransform.localPosition.y, filterTransform.localPosition.z);

            CheckLimits();
        }
    }

    private void CheckLimits()
    {
        if (transform.localPosition.y >= limitPositions.y + limitDistances.y || transform.localPosition.y <= limitPositions.y - limitDistances.y)
        {
            ChangeLayerMask(DEFAULT_LAYER);
            transform.localPosition = filterTransform.localPosition;
        }
        else if (transform.localPosition.z >= limitPositions.z + limitDistances.z || transform.localPosition.z <= limitPositions.z - limitDistances.z)
        {
            ChangeLayerMask(DEFAULT_LAYER);
            transform.localPosition = filterTransform.localPosition;
        }
        else if (filterTransform.localPosition.x <= limitPositions.x - limitDistances.x)
        {
            isGrabbed = false;
            if(finishedPushCalled < finishedPushing.Count){
                finishedPushing[finishedPushCalled].Invoke();
                finishedPushCalled++;
            }
            filterTransform.localPosition = limitPositions;
            ChangeLayerMask(DEFAULT_LAYER);
        }
        else if (filterTransform.localPosition.x >= limitDistances.x + filterLimitX)
        {
            isGrabbed = false;
            if(finishedPushCalled < finishedPushing.Count){
                finishedPushing[finishedPushCalled].Invoke();
                finishedPushCalled++;
            }
            filterTransform.localPosition = new Vector3(filterLimitX - limitDistances.x, 0f, 0f);
            ChangeLayerMask(DEFAULT_LAYER);
        }
    }

    private void ChangeLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (caseRemoved)
        {
            transform.SetParent(parentTransform);
            isGrabbed = true;
        }
        else
        {
            ChangeLayerMask(DEFAULT_LAYER);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ChangeLayerMask(GRAB_LAYER);
        isGrabbed = false;
        transform.localPosition = filterTransform.localPosition;

    }

    public void RemoveCase()
    {
        if (!caseRemoved)
        {
            caseRemoved = true;
        }
    }
}
