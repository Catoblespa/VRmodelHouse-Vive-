using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Incandescent : MonoBehaviour
{
    public bool isLightOn = true; // 에디터에서 조명 on/off 판단

    [Range(0, 7)]
    public float lightPower = 2.5f; // 조명 세기

    void Reset()
    {
        createIncandescent();
    }

    private void OnDrawGizmos() // 게임모드가 아닌 에디터 모드에서 갱신되는 함수
    {
        onOffLightinEditor(); // 에디터에서 조명을 껏다 켰다 할 수 있는 bool 관련 함수
        transform.GetComponentInChildren<Light>().intensity = lightPower; //내가 클릭한 오브젝트에 자식 오브젝트 중 Light컴포넌트가 있는 오브젝트를 검색해서 강도 조절
    }

void createIncandescent() // 노란 조명
    {
        float y_locate; // 라이트 위치 설정할때 사용한 변수
        y_locate = this.transform.localScale.y * 1.2f;

        GameObject thisLight = new GameObject("Light"); // 게임오브젝트를 Light라는 변수 이름으로 생성
        GameObjectUtility.SetParentAndAlign(thisLight, this.gameObject); // 선택한 오브젝트(p2)에 자식으로 p1 오브젝트 삽입
        thisLight.AddComponent<Light>(); // Light 게임오브젝트에 Light 컴포넌트 추가
        thisLight.GetComponent<Light>().type = LightType.Point; // Light오브젝트의 Light컴포넌트에 타입을 포인트라이트로 설정
        thisLight.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0)); // 라이트가 아래를 쳐다보게 설정
        // Light.GetComponent<Light>().intensity = lightPower; // 빛 세기 설정
        
        thisLight.GetComponent<Transform>().localPosition = new Vector3(0, -y_locate, 0); // 설치를 클릭한 오브젝트보다 조금 아래로 설정
        thisLight.GetComponent<Light>().color = new Color32(176, 170, 22, 255);  // 빛 색깔 설정
        thisLight.GetComponent<Light>().shadows = LightShadows.Soft; // 빛이 물체를 투과하는 정도??
    }
 
    public void destroyLight() // 라이트 제거 함수
    {
        for (int i = 0;  i < this.transform.childCount; i++) // 자식 개수 새서 손자까지 말고 자식까지만 검색해서 라이트속성 있는것들 삭제
        {         
            if(transform.GetChild(i).GetComponent<Light>() != null)
            {
                DestroyImmediate(transform.GetChild(i).GetComponent<Light>().gameObject); // 클릭한 자식중 Light 컴포넌트 있는것들 삭제
                DestroyImmediate(this.gameObject); // 이 스크립트 컴포넌트를 가진 오브젝트 삭제
                
                break;
            }     
        }
    }

    public void onOffLightinEditor() // 에디터에서 조명을 껏다 켰다 할 수 있는 bool 관련 함수
    {
        if (isLightOn)
        {
            for (int i = 0; i < this.transform.childCount; i++) // 자식 개수 새서 손자까지 말고 자식까지만 검색해서 라이트속성 있는것들 삭제
            {
                if (transform.GetChild(i).GetComponent<Light>() != null)
                {
                    transform.GetChild(i).GetComponent<Light>().gameObject.GetComponent<Light>().enabled = true;
                    break;
                }               
            }
        }
        else
        {
            for (int i = 0; i < this.transform.childCount; i++) // 자식 개수 새서 손자까지 말고 자식까지만 검색해서 라이트속성 있는것들 삭제
            {
                if (transform.GetChild(i).GetComponent<Light>() != null)
                {
                    transform.GetChild(i).GetComponent<Light>().gameObject.GetComponent<Light>().enabled = false;
                    break;
                }
            }

        }
    }
}
