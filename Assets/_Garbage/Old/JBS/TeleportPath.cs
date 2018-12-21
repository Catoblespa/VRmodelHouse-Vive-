using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPath : MonoBehaviour {

    public bool IsActive { set; get; }

    public int Count;
    public float CurveValue;
    public float Gravity;
    public Vector3 Velocity;
    public Vector3 GroundPos;
    public List<Transform> RenderList = new List<Transform>();   

	// Use this for initialization
	void Start () {

        CreateRender();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsActive == true) ShowPath();
        else HidePath();
    }
    private void CreateRender()
    {
        for(int i=0;i<Count;i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.layer = LayerMask.NameToLayer("Ignore Raycast");
            obj.transform.parent = transform;
            obj.GetComponent<MeshRenderer>().material.color = Color.blue;
            Destroy(obj.GetComponent<BoxCollider>());

            RenderList.Add(obj.transform);
            RenderList[i].gameObject.SetActive(false);
        }
    }
    private void HidePath()
    {
        for(int i=0;i<Count;i++)
        {
            if (RenderList[i].gameObject.activeSelf == false) continue;
            RenderList[i].gameObject.SetActive(false);
        }
    }
    private void ShowPath()
    {
        if (RenderList.Count == 0) CreateRender();

        Vector3 pos = transform.position;
        Vector3 g = new Vector3(0, Gravity, 0);
        Velocity = transform.forward * CurveValue;

        for(int i=0;i<Count;i++)
        {
            //포물선 운동
            //y(높이값)을 구하기위한 식
            // S = s0 + v0t + 1/2gt^2
            float t = i * 0.001f;

            pos = pos + (Velocity * t) + (0.5f * g * t * t);
            Velocity += g;
            RenderList[i].position = pos;
            RenderList[i].gameObject.SetActive(true);
        }
    }
    private void CheckGround()
    {
        int closeIdx = 0;
        float distance = 100f;
        RaycastHit hit;
        GroundPos = Vector3.zero;

        for(int i=0;i<Count;i++)
        {
            if (RenderList[i].gameObject.activeSelf == false) continue;
            if(Physics.Raycast(RenderList[i].position,Vector3.down,out hit,Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground") == false) continue;
                float curDist = Vector3.Distance(RenderList[i].position, hit.point);

                if (distance < curDist) continue;
                closeIdx = i;
                GroundPos = hit.point;
            }
        }
        for (int i = closeIdx; i < Count; i++)
            RenderList[i].gameObject.SetActive(false);        
    }
}
