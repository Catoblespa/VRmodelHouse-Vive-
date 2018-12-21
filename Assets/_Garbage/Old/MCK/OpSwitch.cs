using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpSwitch : MonoBehaviour
{
    public GameObject player = null;
    public bool isSelected;

    public bool isOn;

    public Button ButtonTrue;
    public Button ButtonFalse;

    // Use this for initialization
    void Start () {
        isSelected = false;
        isOn = true;
    }

    void Update()
    {
        if (player != null && isSelected)
            transform.LookAt(player.transform);

        SetSwitch();
    }

    void SetSwitch()
    {
        if (isOn)
        {
            ButtonTrue.gameObject.SetActive(true);
            ButtonFalse.gameObject.SetActive(false);
        }
        else
        {
            ButtonTrue.gameObject.SetActive(false);
            ButtonFalse.gameObject.SetActive(true);
        }
    }

    public void SwitchOn()
    {
        isOn = true;
    }

    public void SwitchOff()
    {
        isOn = false;
    }
}
