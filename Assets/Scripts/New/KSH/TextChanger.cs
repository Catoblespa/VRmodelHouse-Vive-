using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour {

    TextMeshProUGUI text;


	// Use this for initialization
	void Start () {
        text = this.GetComponent<TextMeshProUGUI>();
        text.text = transform.parent.name;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
