using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRH_Base;

public class VRH_Sunlight : MonoBehaviour
{
    [HideInInspector]
    public Light thislight = new Light();
    GameObject Child;

    Vector3 position = new Vector3(0.0f, 3.0f, 0.0f);
    [HideInInspector]
    public Vector3 rotation = new Vector3(50.0f, -30.0f, 0.0f);

    void Reset()
    {
        Child = new GameObject("Light");
        thislight = Child.AddComponent<Light>();
        Child.transform.parent = this.transform;

        Child.transform.position = position;
        Child.transform.localRotation = Quaternion.Euler(rotation);

        thislight.type = LightType.Directional;
        thislight.color = new Color32(255, 244, 214, 255);
        thislight.lightmapBakeType = LightmapBakeType.Mixed;
        thislight.shadows = LightShadows.Soft;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
