using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRH_Base;
using TMPro;

[CustomEditor(typeof(VRH_Sunlight))]
public class VRH_SunlightInspector : Editor
{
    VRH_Sunlight vrh_Sunlight = null;
    Light Sunlight = null;
    float _Intensity = 0.0f;
    Vector3 _rotation = new Vector3();


    private void OnEnable()
    {
        vrh_Sunlight = (VRH_Sunlight)target;
        Sunlight = vrh_Sunlight.thislight;
        _rotation = vrh_Sunlight.rotation;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _Intensity = EditorGUILayout.Slider("Sun Intensity", Sunlight.intensity, 0.0f, 10.0f);
        if(_Intensity <= 0)
        {
            Sunlight.gameObject.SetActive(false);
        }
        else
        {
            Sunlight.gameObject.SetActive(true);
        }
        Sunlight.intensity = _Intensity;

        _rotation = EditorGUILayout.Vector3Field("Sun Rotation", Sunlight.transform.localRotation.eulerAngles);
        Sunlight.transform.localRotation = Quaternion.Euler(_rotation);
    }
}
