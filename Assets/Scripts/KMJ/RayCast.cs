using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward * 8, Color.red);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward * 8, out hit, 8))
        {
            Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.gameObject.name == "lightSwitch")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("EEEE");
                    hit.collider.gameObject.GetComponentInParent<_Switch>().onSwitch();
                        
                        //GetComponent<_Switch>().onSwitch();
                }
               // Debug.Log("스위치를 찾았다.");
            }

       
         }
        
	}
}
