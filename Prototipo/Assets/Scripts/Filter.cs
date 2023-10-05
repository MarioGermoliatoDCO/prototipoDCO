using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
// using UnityEditor.Events;

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

    public List<UnityEvent> cleaned;
    public int cleanedCalled = 0;
    public List<UnityEvent> opened;
    public int openedCalled = 0;
    public List<UnityEvent> closed;
    public int closedCalled = 0;
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
            if (openedCalled < opened.Count)
            {
                opened[cleanedCalled].Invoke();
                openedCalled++;
            }
            statusOpened = true;
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
            if(closedCalled < closed.Count){
                closed[closedCalled].Invoke();
                closedCalled++;
                Debug.Log("Filter closed");
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
                if(cleanedCalled < cleaned.Count){
                    cleaned[cleanedCalled].Invoke();
                    cleanedCalled++;
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
