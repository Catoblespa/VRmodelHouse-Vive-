using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teleport : MonoBehaviour {

    [SerializeField]
    private Transform userPointer;
    [SerializeField]
    private LayerMask ExclusiveLayers;
    [SerializeField]
    private bool ShowDebug = true;
    //[SerializeField]
    //private Transform userMenu;

    private float DebugRay_Length = 5f;
    private float DebugRay_Duration = 1f;
    private float RayLength = 500f;

    private Vector3 hitPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    public void Teleportation()
    {
        if(true)
        {
            Debug.DrawRay(userPointer.position, userPointer.forward * DebugRay_Length, Color.blue, DebugRay_Duration);
        }
        RaycastHit hit = ManagerScript.Instance.HitPos(userPointer, 500f,~ExclusiveLayers);
        if (hit.collider!=null)
        {
            hitPos = hit.point;
            Debug.Log("VR hit : " + hit.collider.name + " "+ hitPos);
            transform.position = hitPos;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
            float yRotation = userPointer.transform.eulerAngles.y;
        //    userMenu.eulerAngles = new Vector3(userMenu.eulerAngles.x, yRotation, userMenu.eulerAngles.z);
        }
    }
    public void MovetoArea(Transform area)
    {
        transform.position = area.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
    }
}
