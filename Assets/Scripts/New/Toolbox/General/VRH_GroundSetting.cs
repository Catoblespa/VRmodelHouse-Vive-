using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRH_GroundSetting : EventTrigger
{

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Teleport>().Teleportation();
    }
}
