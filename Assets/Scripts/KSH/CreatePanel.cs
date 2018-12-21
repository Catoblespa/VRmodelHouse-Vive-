using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class _Furniture
{
    public string Frunituretag;
    public List<GameObject> Prefabs;
    public List<Sprite> PrefabImgs;
    public _Furniture()
    {
        Prefabs = new List<GameObject>();
        PrefabImgs = new List<Sprite>();
    }
}

public class CreatePanel : MonoBehaviour {

    //현재 CreatePanel - FurnitureButtonPanel을 다시 바꾸고 싶다면 _Now를 수정하면 자동으로 갱신됨
    [SerializeField]
    public string _Now = null;

    //[SerializeField]
    //public GameObject[][] _FunitureBord;
    [SerializeField]
    public GameObject emptyButton;

    //[SerializeField]
    //public Furniture[] userInsert;

    [SerializeField]
    public GameObject AddedObjectPanel;

    public GameObject content;

    [SerializeField]
    public List<_Furniture> userInsert2 = new List<_Furniture>();

    [SerializeField]
    public List<GameObject> Item_Buttons_List = new List<GameObject>();

    public void Start()
    {
        LoadDirectoriesName();

        this.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(changed(_Now))
        {
            Panel_Item_Remove();
            Panel_view();
        }
    }

    public void Panel_Item_Remove()
    {
        for (int i = Item_Buttons_List.Count - 1; i >= 0; i--)
        {
            Destroy(Item_Buttons_List[i]);
            Item_Buttons_List.Remove(Item_Buttons_List[i]);
        }
    }


    public void Panel_view()
    {
        for (int i = 0; i < userInsert2.Count; i++)
        {
            if (_Now == userInsert2[i].Frunituretag)
            {
                Panel_Item_Create(userInsert2[i].PrefabImgs, userInsert2[i].Prefabs);
            }
        }
    }

    private static object obj;
    public static bool changed(object T)
    {
        bool result = false;
        if (obj != T)
        {
            obj = T;
            result = true;
        }
        return result;
    }

    //여기에 스크롤뷰 컨텐트 추가할 것~~~~~
    void Panel_Item_Create(List<Sprite> img, List<GameObject> prefab)
    {
        for (int i = 0; i < img.Count; i++)
        {
            GameObject ItemButton = Instantiate(emptyButton,content.transform);
            RectTransform ButtonPos = ItemButton.GetComponent<RectTransform>();
            ItemButton.name = "Item_" + i;
            ItemButton.transform.localScale = new Vector3(1, 1, 1);
            ItemButton.GetComponent<Image>().sprite = img[i];
            ItemButton.GetComponent<IndexButton>().img = img[i];
            ItemButton.GetComponent<IndexButton>().Model = prefab[i];
            Item_Buttons_List.Add(ItemButton);
        }
    }


    public void LoadDirectoriesName()
    {
        DirectoryInfo Info = new DirectoryInfo(@"Assets/Resources/Furniture/model");


        if (Info.Exists)
        {
            DirectoryInfo[] CInfo = Info.GetDirectories();
            foreach (DirectoryInfo info in CInfo)
            {
         //       Debug.Log("접근된 폴더이름: " + info.Name);
                _Furniture tem = new _Furniture();
                tem.Frunituretag = info.Name;
                LoadFileList(tem, info.Name);
                userInsert2.Add(tem);
            }
        }

    }


    public void LoadFileList(_Furniture tem, string folderName)
    {
        string modelFolderName = "Assets/Resources/Furniture/model/" + folderName;
        DirectoryInfo model_di = new System.IO.DirectoryInfo(modelFolderName);

        string spriteFolderName = "Assets/Resources/Furniture/sprites/" + folderName;
        DirectoryInfo sprite_di = new System.IO.DirectoryInfo(spriteFolderName);


        foreach (FileInfo File in model_di.GetFiles())
        {
            //_Furniture tem = new _Furniture();
           // tem.Frunituretag = folderName;

            if (File.Extension.ToLower().CompareTo(".prefab") == 0)
            {
                string FileNameOnly = File.Name.Substring(0, File.Name.Length - 7);
                string FullFileName = File.FullName;


                if (Resources.Load<GameObject>("Furniture/model/" + folderName + "/" + FileNameOnly) == null)
                    Debug.Log("프리팹 로딩 실패");
                else
                    tem.Prefabs.Add(Resources.Load("Furniture/model/" + folderName + "/" + FileNameOnly) as GameObject);

            }


            //userInsert2.Add(tem);
        }

        foreach (FileInfo File2 in sprite_di.GetFiles())
        {
            if (File2.Extension.ToLower().CompareTo(".png") == 0)
            {
                string FileNameOnly = File2.Name.Substring(0, File2.Name.Length - 4);
                string FullFileName = File2.FullName;

              //  Debug.Log(FileNameOnly);

                //Sprite _GO = Resources.Load("Furniture/Sprites/" + folderName + "/" + FileNameOnly)as Sprite;

                if (Resources.Load("Furniture/Sprites/" + folderName + "/" + FileNameOnly) == null)
                    Debug.Log("스프라이트 로딩 실패");
                else
                    tem.PrefabImgs.Add(Resources.Load<Sprite>("Furniture/Sprites/" + folderName + "/" + FileNameOnly));

               // Debug.Log("Assets/Resources/Furniture/Sprites/" + folderName + "/" + FileNameOnly);
                //Debug.Log("target : " + Resources.Load("Furniture/Sprites/" + folderName + "/" + FileNameOnly).name);
            }
        }
    }
}
