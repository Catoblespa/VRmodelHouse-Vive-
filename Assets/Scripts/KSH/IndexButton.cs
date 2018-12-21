using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class IndexButton : MonoBehaviour
{
    private ObjectController objc;
    public GameObject Model;
    private GameObject target;
    private GameObject Create_HierarchyLayer;
    private GameObject addIndexPanel;
    public PlayerHand playerHand;
    public PlayerHand_Vive PlayerHand_vive;
    public AddedObjectsPanel addPanel;
    public Sprite img;

    private bool usingVive = false;

    // Use this for initialization
    void Start () {
        //objc = GameObject.FindWithTag("Player").GetComponent<ObjectController>();
        if (GameObject.FindWithTag("Player").GetComponent<PlayerHand>() != null)
        {
            usingVive = false;
            playerHand = GameObject.FindWithTag("Player").GetComponent<PlayerHand>();
        }
        else
        {
            usingVive = true;
            PlayerHand_vive = GameObject.FindWithTag("Player").GetComponent<PlayerHand_Vive>();
        }
        //    Create_HierarchyLayer = GameObject.FindWithTag("HouseMono");
        addPanel = GameObject.FindWithTag("UImanager").GetComponent<UIManager>().addObjectPanel;
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void ButtonClick()
    {

        //target = objc.Target;
        if (Model == null)
        {
            Debug.Log("IndexButton Script에 model이 할당되지 않았습니다.");
        }
        //새로운 코드
        if (usingVive == true)
        {
            if (PlayerHand_vive.Hand != null)
                Destroy(PlayerHand_vive.Hand);
        }
        else if(usingVive == false)
        {
            if (playerHand.Hand != null)
                Destroy(playerHand.Hand);
        }

        GameObject newFurniture = Instantiate(Model);
        newFurniture.transform.localPosition = new Vector3(0, 0, 0);
        newFurniture.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        newFurniture.name = Model.name;

        addPanel.Item_Adder(newFurniture, img);
        
        if (usingVive == true)
        {
            PlayerHand_vive.Hand = newFurniture;
            Debug.Log("Hands up!");
        }
        else if(usingVive == false)
        {
            playerHand.Hand = newFurniture;
        }
    }
}
