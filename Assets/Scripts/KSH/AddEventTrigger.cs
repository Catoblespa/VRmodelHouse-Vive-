using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddEventTrigger: MonoBehaviour {

    public ObjectController objectController;

    // 해당 오브젝트에 가구오브젝트가 갖어야할 이벤트트리거 컴포넌트 설정을 생성, 적용합니다.
    void Start () {

        //objectController = GameObject.FindWithTag("Player").GetComponent<ObjectController>();
        //EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;
        //entry.callback.AddListener((data) => { objectController.ObjectSelecting(); });
        //eventTrigger.triggers.Add(entry);
    }
}



