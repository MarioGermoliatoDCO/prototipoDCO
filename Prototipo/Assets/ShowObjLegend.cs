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
    private LineRenderer line;
    private Vector3 parentCenter;
    void Start()
    {
        mainCamera = Camera.main;
        line = GetComponent<LineRenderer>();
        // line.SetPositions(new Vector3[] {this.transform.position, this.transform.parent.parent.position});
        parentCenter = this.transform.parent.parent.GetComponent<Collider>().bounds.center;
        line.SetPositions(new Vector3[] {this.transform.position, parentCenter});
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.localScale = Vector3.one * (0.1f + distanceMultiplier) * (Vector3.Distance(this.transform.position, mainCamera.transform.position) * 0.5f);
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
