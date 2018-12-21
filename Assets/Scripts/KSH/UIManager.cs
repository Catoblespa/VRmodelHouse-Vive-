using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Conector
{
    public GameObject addedObjectButton;
    public GameObject addedObject;
}

public class UIManager : MonoBehaviour {


    public List<Conector> _Added = new List<Conector>();
    public AddedObjectsPanel addObjectPanel;
    public CreatePanel createPanel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
