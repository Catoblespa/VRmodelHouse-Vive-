using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpAutoSwitch : MonoBehaviour {

    public GameObject LightGroup;
    public bool isHit;
	// Use this for initialization
	void Start () {
        isHit = false;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f, 1 << LayerMask.NameToLayer("Player")))
        {
            Debug.Log(hit.point);
            isHit = true;            
        }
        else
        {
            isHit = false;
        }

        if(isHit)
            LightGroup.SetActive(true);
        else
            LightGroup.SetActive(false);
    }
}
