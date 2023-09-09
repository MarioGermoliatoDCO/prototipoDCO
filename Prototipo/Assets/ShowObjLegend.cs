using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowObjLegend : MonoBehaviour
{
    public float distanceMultiplier = 1;
    private Camera mainCamera;
    public Vector3 offset;
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

    public void ShowLegend(GameObject obj)
    {
        this.gameObject.SetActive(true);
        Vector3 extents = obj.GetComponent<Collider>().bounds.extents;
        Vector3 pos = obj.GetComponent<Collider>().bounds.center + new Vector3(extents.x * offset.x, extents.y * offset.y, extents.z * offset.z);
        this.gameObject.GetComponentInChildren<TMP_Text>().text = obj.name;
        this.transform.position = pos;
    }

    public void HideLegend()
    {
        this.gameObject.SetActive(false);
    }
}
