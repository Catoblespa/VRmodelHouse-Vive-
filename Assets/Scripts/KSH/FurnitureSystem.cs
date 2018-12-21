using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public struct Furniture
{
    public string Frunituretag;
    public GameObject[] Prefab;
    public Sprite[] PrefabImg;
}
public class FurnitureSystem : MonoBehaviour {
    
    public GameObject Player;
    public AddEventTrigger eventTrigger;
    public Furniture[] userInsert;

    //public GameObject[] Prefab_sofa;
    //public Sprite[] Image_sofa;


    //public GameObject[] Prefab_bed;
    //public Sprite[] Image_bed;

    //public GameObject[] Prefab_chair;
    //public Sprite[] Image_chair;

    private void Start()
    {
        AutoAdd_EventTriggerScript();
    }

    void AutoAdd_EventTriggerScript()
    {
        for (int i = 0; i<userInsert.Length; i++)
        {
            for (int j = 0; j < userInsert[i].Prefab.Length; j++)
            {
                if (userInsert[i].Prefab[j].GetComponent<AddEventTrigger>() == null)
                {
                    userInsert[i].Prefab[j].AddComponent<AddEventTrigger>();
                }
            }
        }
    }
}

