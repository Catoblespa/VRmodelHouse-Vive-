using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRH_Base; // 이 프로젝트를 위해 만든 네임스페이스, 민창기 작성.
using Valve.VR;

public class PlayerHand_Vive : MonoBehaviour
{


    [SerializeField]
    public bool ViveMode = true;


    [SerializeField]
    private LayerMask ExclusiveLayers;

    [SerializeField]
    public GameObject Hand;

    [SerializeField]
    public Bezier bezier;

    [SerializeField]
    public Transform LayStartPos;

    RaycastHit hit;
    private Transform PlayerLook;
    private Transform Arrow;

    [HideInInspector]
    private ViveInput viveInput;

    [HideInInspector]
    public bool moveingFurni = false;


    float timeSpan;
    float checkTime;
    // UsecheckTime this for initialization
    void Start()
    {
        Hand = null;
        if (bezier != null)         //vive모드에서는 bezier를 null로 둠으로써 비활성화
        {
            bezier = bezier.GetComponent<Bezier>();
        }
        viveInput = GameObject.Find("Action").GetComponent<ViveInput>();

        timeSpan = 0.0f;
        checkTime = 5.0f;  // 특정시간을 5초로 지정
        
    }

    // Update is called once per frame
    void Update()
    {
        hit = ManagerScript.Instance.HitPos(LayStartPos, 500f, 1<<9);
        //PlayerLook.position = ;

        if (Hand == null)
            moveingFurni = false;

        if (Hand != null)
        {
            timeSpan += Time.deltaTime;
            moveingFurni = true;
            Debug.Log(timeSpan);
            if (moveingFurni == true)
            {
                if (ViveMode == true)
                {
                    Hand.transform.position = hit.point;
                }
                else
                {
                    Hand.transform.position = Arrow.transform.position;
                }
            }

            if ((Input.GetMouseButtonDown(0)
                || SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
                && timeSpan > 1f)
            {
                Debug.Log("클릭클릭");
                moveingFurni = false;
                Hand = null;
                timeSpan = 0;
            }
        }
    }

    //오브젝트를 감지하기 위해 만든 함수

    static GameObject hitObject = null;
    public void HitObject()
    {
        hit = ManagerScript.Instance.HitPos(LayStartPos, 500f, ~ExclusiveLayers);
        hitObject = hit.transform.gameObject;
        if (hitObject != null && hitObject.GetComponentInParent<VRH_ObjectBase>() != null)
        {
            int hitLayer = hitObject.layer;
            switch (hitLayer)
            {
                case 14:    //Door의 레이어 번호
                            //Debug.Log("Door Contect");
                    hitObject.GetComponentInParent<VRH_ObjectBase>().isSelected = true;

                    break;
            }
        }

    }

    public void ReleaseObject()
    {
        if (hitObject != null && hitObject.GetComponentInParent<VRH_ObjectBase>() != null)
            hitObject.GetComponentInParent<VRH_ObjectBase>().isSelected = false;
        hitObject = null;
    }
}
