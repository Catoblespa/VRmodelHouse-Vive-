using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spot_Light))] // inspector 편집할 스크립트 넣어주기.
[CanEditMultipleObjects]
public class VRH_Spot_Light : Editor
{

    //   SerializedProperty slight;

    public override void OnInspectorGUI()
    {
        //      serializedObject.Update();
        //     serializedObject.ApplyModifiedProperties();
        GameObject Target = Selection.activeObject as GameObject;


        base.OnInspectorGUI();

        if (GUILayout.Button("Delete")) // 클릭 시 스크립트 삭제 + 게임오브젝트 삭제가 되어야 한다. 
        {
            Debug.Log("ghjg");
            Target.GetComponent<Spot_Light>().destroyLight();

            //   GameObject g = GameObject.Find("SpotLight");
            //  Destroy(g);
            //GameObject.Find("SpotLight").GetComponent<Light>().intensity = GameObject.Find("Cube").GetComponent<Slight>().lightPower;
            // 구현 못함.
            //Destroy(this);           
        }
    }
}
