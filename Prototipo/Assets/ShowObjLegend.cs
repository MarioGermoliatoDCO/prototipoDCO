using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowObjLegend : MonoBehaviour
{
    public float distanceMultiplier = 1;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = Vector3.one * distanceMultiplier * Vector3.Distance(this.transform.position, mainCamera.transform.position);
    }

    public void ShowLegend(GameObject obj){
        this.gameObject.SetActive(true);
        Vector3 pos = obj.GetComponent<Collider>().bounds.center + Vector3.up * obj.GetComponent<Collider>().bounds.extents.y;
        this.gameObject.GetComponentInChildren<TMP_Text>().text = obj.name;
        this.transform.position = pos;
    }

    public void HideLegend(){
        this.gameObject.SetActive(false);
    }
}
