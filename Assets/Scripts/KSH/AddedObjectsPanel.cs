using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class AddedObjectsPanel : MonoBehaviour {


    [SerializeField]
    public GameObject content;

    [SerializeField]
    public GameObject ButtonPrefab;

    [SerializeField]
    public UIManager manager;


    private int counter = 0;
    private List<Conector> _Added;


    private void Start()
    {
        manager = GameObject.FindWithTag("UImanager").GetComponent<UIManager>();
        _Added = manager._Added;
    }


    public void Item_Adder(GameObject Item, Sprite img)
    {

        Conector temCon;
        temCon.addedObjectButton = Instantiate(ButtonPrefab, content.transform);
        temCon.addedObjectButton.name = "AddedObjectButton (" + counter +")";

        temCon.addedObjectButton.GetComponent<Image>().sprite = img;
        temCon.addedObject = Item;
        _Added.Add(temCon);

        if (temCon.addedObjectButton.GetComponent<addedObjectButton>() != null)
        {
            temCon.addedObjectButton.GetComponent<addedObjectButton>().con = temCon;
        }
        else
            Debug.Log("myDebug : temCon.addedObjectButton.GetComponent<addedObjectButton>() is null !!");

    }


    public void Item_Remove(GameObject Item)
    {
        //for (int i = _Added.Count - 1; i >= 0; i--)
        //{
        //    if (Item.name == _Added[i].addedObject)
        //    Destroy(Item_Buttons_List[i]);
        //    Item_Buttons_List.Remove(Item_Buttons_List[i]);
        //}
    }
}
