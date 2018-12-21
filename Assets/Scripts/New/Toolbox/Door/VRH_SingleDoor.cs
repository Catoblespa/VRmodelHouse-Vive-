using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using VRH_Base;
using TMPro;
using UnityEngine.EventSystems;

public class VRH_SingleDoor : VRH_DoorBase
{
    [HideInInspector] public StartTransform startTransform = new StartTransform();
    [HideInInspector] public float OpenSpeed = 10;
    [HideInInspector] public bool isOpened = false;

    [HideInInspector] public HINGE hinge = HINGE.Manual;
    [HideInInspector] public Vector3 HingePostion = new Vector3();

    [HideInInspector] public AXIS axis = AXIS.Y_AXIS;
    [HideInInspector] public bool isLeftDoor = true;
    [HideInInspector] public int AngleDegree = 90;

    [HideInInspector] public GameObject subscript;
    [HideInInspector] public string SubscriptText = null;
    [HideInInspector] public Vector3 SubscriptPosition = new Vector3();
    [HideInInspector] public Vector3 SubscriptRotation = new Vector3();

    VRH_ObjectBase objectBase;
    [HideInInspector] public bool isSelected = false;
    private float time = 0.0f;
    private bool isMove = false;


    private void Reset()
    {
        HingePostion = transform.position;
        SubscriptPosition = transform.position;
        startTransform = new StartTransform(transform.position, transform.rotation);
        MeshRenderer[] ChildObject = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < ChildObject.Length; ++i)
        {
            if (ChildObject[i].GetComponent<MeshRenderer>() && !ChildObject[i].GetComponent<MeshCollider>())
                ChildObject[i].gameObject.AddComponent<MeshCollider>();

            ChildObject[i].gameObject.layer = LayerMask.NameToLayer("Door");

        }

        if (!transform.GetComponent<VRH_ObjectBase>())
        {
            transform.gameObject.AddComponent<VRH_ObjectBase>();
        }
    }

    private void Start()
    {
        startTransform = new StartTransform(transform.position, transform.rotation);
        GetSubscriptTextureTransform(SubscriptText);


        objectBase = GetComponent<VRH_ObjectBase>();
        if (objectBase != null)
            objectBase.isSelected = false;
    }

    private void Update()
    {
        if (objectBase == null)
            return;

        isSelected = objectBase.isSelected;
        OnUI(isSelected);

        if (isSelected && !isMove && Input.GetMouseButtonDown(0))
        {
            isMove = true;
        }

        if (isMove)
        {
            if (time < AngleDegree)
            {
                time += Time.deltaTime * OpenSpeed;
                if (isOpened)
                {
                    transform.RotateAround(HingePostion, Door.GetAsix(axis, !isLeftDoor), Time.deltaTime * OpenSpeed);
                }
                else
                {
                    transform.RotateAround(HingePostion, Door.GetAsix(axis, isLeftDoor), Time.deltaTime * OpenSpeed);
                }
            }
            else
            {
                time = 0;
                isMove = false;
                isOpened = !isOpened;
            }
        }
    }



    void OnUI(bool _isSelected)
    {
        if (_isSelected)
        {
            subscript.SetActive(true);
        }
        else
        {
            subscript.SetActive(false);
        }
    }

    Transform CreateSubscriptTransform()
    {
        if (subscript == null)
        {
            subscript = Instantiate(Resources.Load("UI/SubscriptUI")) as GameObject;
            subscript.transform.parent = transform;
            subscript.transform.position = SubscriptPosition;
            subscript.SetActive(false);
        }
        return subscript.transform;
    }

    protected override void GetSubscriptTextureTransform(string _Subscript)
    {
        Transform_UITexture = CreateSubscriptTransform();
        TextMeshProUGUI[] textMeshs = new TextMeshProUGUI[2];

        if (Transform_UITexture.GetComponentsInChildren<TextMeshProUGUI>() != null)
        {
            textMeshs = Transform_UITexture.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < 2; ++i)
            {
                textMeshs[i].text = _Subscript;
            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHand>().HitObject();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHand>().ReleaseObject();
    }


}
