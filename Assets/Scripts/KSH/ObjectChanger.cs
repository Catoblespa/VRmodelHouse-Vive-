using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChanger : MonoBehaviour {


    public float Radius;
    public GameObject UI;
    public float Movement;

    private Transform centerPos;
    private Transform radiusPos;
    private Transform playerPos;
    private GameObject view_Object;


    public GameObject[] FurnitureIndex;


    private 

    // Use this for initialization
    void Start ()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        view_Object = transform.Find("Model").gameObject;
        UI_Posistioning();


    }

    // Update is called once per frame
    void Update () {
        UiMover();
    }


    void UiMover()
    {
        Vector3 targetPostition = new Vector3(playerPos.position.x, centerPos.transform.position.y, playerPos.position.z);
        centerPos.transform.LookAt(targetPostition);
        radiusPos.transform.LookAt(playerPos);
    }

    

    void UI_Posistioning()
    {

        if (Radius != 0)
        {
            Debug.Log(this.gameObject.name + "의  UI 위치값 입력 완료");
            centerPos = transform.Find("Center");
            radiusPos = centerPos.transform.Find("Radius");
            radiusPos.transform.localPosition = new Vector3(0, 1.0f, Radius);
        }
        else
        {
         //   Debug.Log(this.gameObject.name + "의  UI 위치값 없음");
        }

       // Debug.Log("Center Pos : (" + centerPos.position.x + "," + centerPos.position.y + "," + centerPos.position.z + ")");
       // Debug.Log("Radius Pos : (" + radiusPos.position.x + "," + radiusPos.position.y + "," + radiusPos.position.z + ")");
    }

    public void Changer(int NUM)
    {
        Destroy(view_Object);

        GameObject New_Object = Instantiate(FurnitureIndex[NUM], this.transform);
        New_Object.name = "Model";
        view_Object = New_Object;
        if (view_Object == null)
        {
            //view_Object = transform.Find("Model").gameObject;
           // Debug.Log(view_Object.name);
        }
    }


    void ActivityON()
    {

    }

    void ActivityOff()
    {

    }


    public void RotateY_Minus()
    {
        this.transform.Rotate(this.transform.rotation.x,-90,this.transform.rotation.z);

    }
    public void RotateY_Plus()
    {
        this.transform.Rotate(this.transform.rotation.x, 90, this.transform.rotation.z);
    }
    public void PosX_Plus()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x+ Movement, this.transform.position.y, this.transform.position.z);
    }
    public void PosX_Minus()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x- Movement, this.transform.position.y, this.transform.position.z);
    }
    public void PosZ_Plus()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z+ Movement);
    }
    public void PosZ_Minus()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z- Movement);
    }


}
