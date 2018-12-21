using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour {

    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;
    public SteamVR_Action_Single MenuAction;
    public GameObject MainUI;

    private PlayerHand_Vive playerHand_vive;

    [HideInInspector]
    public bool LeftTriggerDown = false;
    [HideInInspector]
    public bool RightTriggerDown = false;
    private void Start()
    {
        playerHand_vive = GameObject.FindWithTag("Player").GetComponent<PlayerHand_Vive>();
        //MainUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        float triggerValueLeft = squeezeAction.GetAxis(SteamVR_Input_Sources.LeftHand);
        float triggerValueRight = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);
        Vector2 touchpadValueRigth = touchPadAction.GetAxis(SteamVR_Input_Sources.RightHand);
        //float menuValue = MenuAction.GetAxis(SteamVR_Input_Sources.Any);

        if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("트랙패드 클릭 (왼쪽)");
        }

        if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            print("트랙패드 클릭 (오른쪽)");
        }



        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log("트리거 왼쪽");
        }

        
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            //if (playerHand_vive.Hand != null)
            //{
            //    Debug.Log("Hands down");
            //    playerHand_vive.moveingFurni = false;
            //    playerHand_vive.Hand = null;
            //}
            Debug.Log("트리거 오른쪽");
        }
        


        if (SteamVR_Input._default.inActions.MenuButton.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("메뉴 버튼 클릭 (왼쪽)");
            if (MainUI.activeSelf == true)
                MainUI.SetActive(false);
            else
                MainUI.SetActive(true);
        }

        if (SteamVR_Input._default.inActions.MenuButton.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (playerHand_vive.Hand != null)
            {
                Destroy(playerHand_vive.Hand);
            }
            print("메뉴 버튼 클릭 (오른쪽)");
        }



        if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("그립 (왼쪽)");
        }
        if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
        {

            print("그립 (오른쪽)");
        }



        //tirrger를 외부로 이벤트
        if (triggerValueLeft == 1.0f)            LeftTriggerDown = true;
        else                                    LeftTriggerDown = false;

        if (triggerValueRight == 1.0f)
        {
            RightTriggerDown = true;
        }
        else RightTriggerDown = false;

        //if(menuValue > 0.0f)
        //{
        //    print("menuButton Down!!!");
        //    if (MainUI.activeSelf == true)
        //        MainUI.SetActive(false);
        //    else
        //        MainUI.SetActive(true);
        //}



        //터치패드 Vector2값 가져오기.
        if (touchpadValueRigth != Vector2.zero)
        {
            //print(touchpadValueRigth);
            if (playerHand_vive.Hand != null)
            {
                if (touchpadValueRigth.x > 0.0f)     //트랙패드 왼쪽
                {
                    playerHand_vive.Hand.transform.Rotate(Vector3.up * Time.deltaTime *30 * touchpadValueRigth.x);
                }
                else if (touchpadValueRigth.x < 0.0f)
                {
                    playerHand_vive.Hand.transform.Rotate(Vector3.up * Time.deltaTime *30 * touchpadValueRigth.x);
                }
            }
        }
    }

}
