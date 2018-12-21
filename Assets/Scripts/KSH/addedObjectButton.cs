using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addedObjectButton : MonoBehaviour
{


    public Conector con;
    public GameObject Text;
    private bool OndClick;
    

    // Use this for initialization
    void Start()
    {
        Text.SetActive(false);
        OndClick = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ButtonClick()
    {
        Text.SetActive(true);
        Invoke("TextSetFalse", 2.0f);
        if (OndClick)
        {
            Destroy(con.addedObject);
            Destroy(con.addedObjectButton);     //this object!
        }
        OndClick = true;
    }

    public void TextSetFalse()
    {
        Text.SetActive(false);
        OndClick = false;
    }
}

