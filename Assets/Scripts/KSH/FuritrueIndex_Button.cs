using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FuritrueIndex_Button : MonoBehaviour {


    private string _myName;


    private CreatePanel _createMenu;
    [SerializeField]
    public bool isMainElement = true;


    [SerializeField]
    public GameObject _mainPanel;
    [SerializeField]
    public GameObject _createPanel;
    


    // Use this for initialization
    void Start () {
        _myName = this.gameObject.name;
        _createPanel = GameObject.Find("MainUI").transform.Find("CreatePanel").gameObject;
        _mainPanel = GameObject.Find("MainUI").transform.Find("MainPanel").gameObject;
        _createMenu = _createPanel.GetComponent<CreatePanel>();


     //   textstring = TextObj.GetComponent<TextMeshPro>().text;
    }
    



    public void isClick()
    {
        //_createMenu.Panel_Item_Remove();

        if (isMainElement == true)
        {
            _createMenu._Now = _myName;
            //_createPanel.GetComponent<CreatePanel>().Panel_view();
            _mainPanel.SetActive(false);
            _createPanel.SetActive(true);
        }
        else if (isMainElement == false)
        {
            _createMenu._Now = _myName;
           // _createPanel.GetComponent<CreatePanel>().Panel_view();
        }
    }
}
