using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEditor.Events;

public class Filter : MonoBehaviour
{
    [SerializeField] private MeshRenderer filterMesh;
    [SerializeField] private Material cleanFilterMesh;
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private BoxCollider filterCollider;
    [SerializeField] XRSocketInteractor filterCaseSocket;

    private const string BRUSH_TAG = "Brush";

    private float currentProgressToClean;
    private float timeToClean = 5f;

    private AudioSource audioSource;

    public UnityEvent cleaned;
    public UnityEvent opened;
    public UnityEvent closed;
    private bool statusOpened = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckFilterOpen();
        CheckFilterClosed();
    }

    private void CheckFilterOpen()
    {
        if (transform.localPosition.x >= 1.3f)
        {
            filterCollider.enabled = true;
            opened.Invoke();
            statusOpened = true;
            for (int i = 0; i < opened.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(opened, i);
            }
        }
        else
        {
            filterCollider.enabled = false;
        }
    }

    private void CheckFilterClosed()
    {
        if (transform.localPosition.x <= 0.1f && statusOpened)
        {
            filterCaseSocket.enabled = true;
            closed.Invoke();
            Debug.Log("Filter closed");
            for (int i = 0; i < closed.GetPersistentEventCount(); i++)
            {
                UnityEventTools.RemovePersistentListener(closed, i);
            }
        }
        else
        {
            filterCaseSocket.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == BRUSH_TAG)
        {
            dustParticles.SetActive(true);
            audioSource.volume = 1;
            if (currentProgressToClean < timeToClean)
            {
                dustParticles.SetActive(true);
                currentProgressToClean += Time.deltaTime;
            }
            else
            {
                filterMesh.material = cleanFilterMesh;
                cleaned.Invoke();
                for (int i = 0; i < cleaned.GetPersistentEventCount(); i++)
                {
                    UnityEventTools.RemovePersistentListener(cleaned, i);
                }
                dustParticles.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioSource.volume = 0;
        dustParticles?.SetActive(false);
    }
}
