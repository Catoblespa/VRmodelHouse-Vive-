using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpLightSwitch : MonoBehaviour {

    public OpSwitch SwitchPrefab;
    public GameObject LightGroup;

    public bool onLight;

	// Use this for initialization
	void Start () {
        onLight = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(LightGroup != null)
            SetLight();
    }

    void SetLight()
    {
        if(onLight)
        {
            LightGroup.SetActive(true);
        }
        else
        {
            LightGroup.SetActive(false);
        }
    }

    public void LightOn()
    {
        onLight = true;
    }

    public void LightOff()
    {
        onLight = false;
    }
}
