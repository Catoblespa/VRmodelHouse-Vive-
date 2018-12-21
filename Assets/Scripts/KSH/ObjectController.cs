using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectController : MonoBehaviour
{

    //    public Vector3 movePos;
    public GameObject FurnitureUI;
    public GameObject MainMenu;
    public GameObject Item_Panel;
    public bool IsMove = false;

    private FurnitureSystem furnitureSystem;
    private GameObject userCamera;
    public GameObject emptyButton;

    float Posx = -30f;
    float Posy = 35f;

    [SerializeField]
    public GameObject Target;
    [SerializeField]
    public List<GameObject> Item_Buttons = new List<GameObject>();
    [SerializeField]
    private LayerMask ExclusiveLayers;

    void Start()
    {
        userCamera = GameObject.FindWithTag("MainCamera").gameObject;
        furnitureSystem = GameObject.FindWithTag("furnitureSystem").GetComponent<FurnitureSystem>();
    }
    void Update()
    {
        MoveFuniture();
    }
    public void ObjectSelecting()
    {
        RaycastHit hit = ManagerScript.Instance.HitPos(userCamera.transform, Mathf.Infinity, ~ExclusiveLayers);
        if(hit.collider!=null)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("furniture"))
            {
                //Debug.Log("dawd");
                if (Target != null)// && Target.name == hit.transform.name)
                {
                    for (int i = Item_Buttons.Count - 1; i >= 0; i--)
                    {
                        Destroy(Item_Buttons[i]);
                        Item_Buttons.Remove(Item_Buttons[i]);
                    }
                    Target = null;
                    Posx = -30f;
                    Posy = 35f;
                }
                FurnitureUI.SetActive(true);
                MainMenu.SetActive(false);
                Target = hit.transform.gameObject;
                Panel_view();
            }
            //MCk 가 추가한 부분
            SelectSwitch(hit.transform.gameObject);
        }    
    }
    void Panel_view()
    {
        for(int i=0;i<furnitureSystem.userInsert.Length;i++)
        {
            if (Target.tag == furnitureSystem.userInsert[i].Frunituretag)
                Panel_Item(furnitureSystem.userInsert[i].PrefabImg,furnitureSystem.userInsert[i].Prefab);
        }
    }
    void Panel_Item(Sprite[] img,GameObject[] prefab)
    {
        for (int i = 0; i < img.Length; i++)
        {

            GameObject ItemButton = Instantiate(emptyButton, Item_Panel.transform);
            RectTransform ButtonPos = ItemButton.GetComponent < RectTransform>();
            ItemButton.name = "Item_" + i;
            ItemButton.GetComponent<Image>().sprite = img[i];
            ItemButton.GetComponent<IndexButton>().Model = prefab[i];
            ButtonPos.localPosition = new Vector3(Posx, Posy, 0);
            Posx += 30;
            if (i % 3 == 2)
            {
                Posx = -30f;
                Posy -= 30f;
            }
            Item_Buttons.Add(ItemButton);
        }
    }

    public void SwitchMove()
    {
        if (IsMove == false)
            IsMove = true;
        else
            IsMove = false;
    }

    private void MoveFuniture()
    {
        if(IsMove)
        {
            Target.transform.gameObject.layer = 2;
            RaycastHit hit = ManagerScript.Instance.HitPos(userCamera.transform, Mathf.Infinity, ~ExclusiveLayers);
            if (hit.collider!=null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    IsMove = false;
                    Target.transform.gameObject.layer = 11;
                    //Target.transform.parent.position = new Vector3(Target.transform.position.x, Target.transform.position.y - 0.2f, Target.transform.position.z);
                }
                else
                {
                    Target.transform.parent.position = hit.point;
                }
            }
        }    
    }

    public void RotateY_Plus()
    {
        Target.transform.Rotate(Target.transform.rotation.x, 90, Target.transform.rotation.z);

    }
    public void RotateY_Minus()
    {
        Target.transform.Rotate(Target.transform.rotation.x, -90, Target.transform.rotation.z);
    }


    //MCk 가 만든 함수
    public void SelectSwitch(GameObject target)
    {
        if (target.layer == LayerMask.NameToLayer("Switch"))
        {
            target.GetComponent<OpSwitch>().isSelected = true;
            target.GetComponent<OpSwitch>().player = gameObject;
        }
    }
}
