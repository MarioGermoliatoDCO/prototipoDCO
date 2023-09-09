using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Step
{
    public AudioClip Narration;
    public float duration;
    public UnityEvent onEnd;
}

public class SequenceEvent : MonoBehaviour
{
    public List<Step> steps;
    public int actualStep = 0;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(NextStep());
    }

    public void CallNextStep()
    {
        if (actualStep < steps.Count)
        {

            actualStep++;
            StartCoroutine(NextStep());
        }
    }

    IEnumerator NextStep()
    {
        if (steps[actualStep].Narration != null)
        {
            audio.clip = steps[actualStep].Narration;
            audio.Play();
            yield return new WaitForSeconds(audio.clip.length);
            steps[actualStep].onEnd.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
