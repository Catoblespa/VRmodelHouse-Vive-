using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class addMenuButton : MonoBehaviour {




    public GameObject defaultButton;
    public GameObject content;
    

    // Use this for initialization
    void Start () {
        LoadDirectoriesName();
    }

    public void LoadDirectoriesName()
    {
        DirectoryInfo Info = new DirectoryInfo(@"Assets/Resources/Furniture/model");
        if (Info.Exists)
        {
            DirectoryInfo[] CInfo = Info.GetDirectories();
            foreach (DirectoryInfo info in CInfo)
            {
                Debug.Log("인덱스 접근 폴더이름: " + info.Name);
                GameObject ItemButton = Instantiate(defaultButton, content.transform);
                ItemButton.name = info.Name;
            }
        }
    }
}
