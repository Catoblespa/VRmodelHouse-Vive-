using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

    [SerializeField]
    public GameObject userPointer;

	public static ManagerScript Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ManagerScript>();
                if (!instance)
                {
                    GameObject InstanceContainer = new GameObject("ManagerScript");
                    instance = InstanceContainer.AddComponent<ManagerScript>();
                }

            }
            return instance;
        }
    }
        private static ManagerScript instance;
    //레이캐스트 함수

    public RaycastHit HitPos(Transform obj,float length,LayerMask exclusiveLayer)
    {
        RaycastHit hit=new RaycastHit();
        if(Physics.Raycast(obj.position,obj.forward,out hit,length,exclusiveLayer))
        {
            Debug.DrawRay(obj.position, obj.forward * hit.distance, Color.black);
            return hit;
        }
        return hit;
    }
        
   }

