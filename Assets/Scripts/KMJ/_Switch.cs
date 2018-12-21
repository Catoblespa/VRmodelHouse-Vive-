/*
    한 스위치로 여러개의 조명을 킬수 있게 할수도 있다.
    조명 추가 버튼을 추가 생성하여 public 조명 삽입 칸이 늘어나고 거기에 조명을 추가
    근데 잘 모르겠다.    

    RayCast.cs
    _light.cs 와 연관
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class _Switch : MonoBehaviour {


    [HideInInspector]
    public bool isconnectedSwitchOn;

    public GameObject connectedLight; // 스위치를 달을 조명 넣는 곳



    void Reset()
    {      
        createLightSwitch(); // 스위치 생성
    }

    // Use this for initialization
    void Start()
    {
        isconnectedSwitchOn = connectedLight.GetComponent<Light>().enabled;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createLightSwitch() // 스위치 투명 충돌 생성 함수
    {
        GameObject lightSwitch = new GameObject("lightSwitch");
        GameObjectUtility.SetParentAndAlign(lightSwitch, this.gameObject);
        lightSwitch.AddComponent<BoxCollider>(); // 클릭한 오브젝트 크기에 비례하게 생성됨
    }

    public void destroySwtich() // 스위치 제거
    {
        for (int i = 0; i < this.transform.childCount; i++) // 자식 개수 새서 손자까지 말고 자식까지만 검색
        {
            if (transform.GetChild(i).name == "lightSwitch") //lightSwitch 오브젝트 검색
            {
                DestroyImmediate(transform.GetChild(i).gameObject); // 스위치 오브젝트 제거
                DestroyImmediate(this); // 이 스크립트 컴포넌트를 삭제
                break;
            }
        }
    }

    public void onSwitch()
    {
        if(isconnectedSwitchOn)
        {
            connectedLight.GetComponent<Light>().enabled = false;
            isconnectedSwitchOn = false;
        }
        else
        {
            connectedLight.GetComponent<Light>().enabled = true;
            isconnectedSwitchOn = true;
        }
    }

   

}
