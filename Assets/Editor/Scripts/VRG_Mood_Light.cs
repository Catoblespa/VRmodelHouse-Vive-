using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mood_Lighting))] // inspector 편집할 스크립트 넣어주기.
[CanEditMultipleObjects]
public class VRH_Mood_Light : Editor
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
            Target.GetComponent<Mood_Lighting>().destroyLight();          
        }
    }
}
