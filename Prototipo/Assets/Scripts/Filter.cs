using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
        }
        else
        {
            filterCollider.enabled = false;
        }
    }

    private void CheckFilterClosed()
    {
        if (transform.localPosition.x == 0)
        {
            filterCaseSocket.enabled = true;
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
