using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParafusosController : MonoBehaviour
{
    public Screw[] parafusos;
    
    [SerializeField]
    private int ActiveScrew = -1;
    // Start is called before the first frame update
    void Start()
    {
        parafusos = gameObject.GetComponentsInChildren<Screw>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateNextScrew(){
        ActiveScrew++;
        if (ActiveScrew >= parafusos.Length){
            ActiveScrew = 0;
        }
        Screw screw = parafusos[ActiveScrew];
        screw.GetComponent<Outline>().enabled = true;
        screw.GetComponent<Collider>().enabled = true;
        if (ActiveScrew > 0){
            Screw lastScrew = parafusos[ActiveScrew - 1];
            lastScrew.GetComponent<Outline>().enabled = false;
            lastScrew.GetComponent<Collider>().enabled = false;
        }
    }
}
