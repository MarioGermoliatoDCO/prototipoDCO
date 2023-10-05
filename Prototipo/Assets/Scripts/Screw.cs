using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// using UnityEditor.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class Screw : MonoBehaviour
{
    [SerializeField] GameObject screwSocket;
    [SerializeField] XRGrabInteractable grabInteractable;

    public List<UnityEvent> onDetach;
    public int onDeatchCalled;
    public List<UnityEvent> onRemove;
    public int onRemoveCalled;
    public List<UnityEvent> onAttach;
    public int onAttachCalled;
    public List<UnityEvent> onFixed;
    public int onFixedCalled;
    private const string SCREWDRIVER_TAG = "Screwdriver";
    private float timeToRemoveOrPlaceScrew = 3f;
    private float currentProgressToRemove = 0;
    private float positionAfterRemoved = -7.66f;
    private bool isScrewRemoved;
    private bool isScrewPlacedAgain;
    private float currentProgressToPlace = 0;
    private float positionAfterPlaced = -7.673f;
    private Rigidbody screwRigidbody;
    public Transform tablePos;

    public static event EventHandler OnScrewRemoved;

    private AudioSource audio;

    private void Start()
    {
        screwRigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    public void ChangeIsPlacedAgain()
    {
        if (onAttachCalled < onAttach.Count)
        {
            onAttach[onAttachCalled].Invoke();
            onAttachCalled++;

        }
        isScrewPlacedAgain = true;
    }

    public bool GetScrewRemoved()
    {
        return isScrewRemoved;
    }

    public void ScrewGrab()
    {
        if (!isScrewRemoved)
        {
            isScrewRemoved = true;
            if (onRemoveCalled < onRemove.Count)
            {
                Debug.Log("removed");
                onRemove[onRemoveCalled].Invoke();
                onRemoveCalled++;
                OnScrewRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void RemoveScrew(Collider other)
    {
        if (other.gameObject.tag == SCREWDRIVER_TAG && !isScrewRemoved)
        {
            if (currentProgressToRemove < timeToRemoveOrPlaceScrew)
            {
                currentProgressToRemove += Time.deltaTime;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 50, transform.eulerAngles.y, transform.eulerAngles.z);
                audio.volume = 0.7f;
            }
            else
            {
                transform.position = new Vector3(positionAfterRemoved, transform.position.y, transform.position.z);
                grabInteractable.enabled = true;
                if (onDeatchCalled < onDetach.Count)
                {
                    onDetach[onDeatchCalled].Invoke();
                    onDeatchCalled++;
                    audio.volume = 0;
                    Debug.Log("Parafuso removido");
                }
            }
        }
    }

    private void PlaceScrew(Collider other)
    {
        if (other.gameObject.tag == SCREWDRIVER_TAG && isScrewPlacedAgain)
        {
            if (currentProgressToPlace < timeToRemoveOrPlaceScrew)
            {
                currentProgressToPlace += Time.deltaTime;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - 50, transform.eulerAngles.y, transform.eulerAngles.z);
                audio.volume = 0.7f;
            }
            else
            {
                if (onFixedCalled < onFixed.Count)
                {

                    transform.position = new Vector3(positionAfterPlaced, transform.position.y, transform.position.z);
                    grabInteractable.enabled = false;
                    audio.volume = 0;
                    onFixed[onFixedCalled].Invoke();
                    onFixedCalled++;
                    Debug.Log("Parafuso recolocado");
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        RemoveScrew(other);
        PlaceScrew(other);
    }

    private void OnTriggerExit(Collider other)
    {
        audio.volume = 0;
    }

    public void ReleaseScrew()
    {
        if (!isScrewPlacedAgain)
        {
            this.transform.position = tablePos.position;
        }
    }
}
