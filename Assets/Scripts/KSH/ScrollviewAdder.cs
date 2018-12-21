using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollviewAdder : MonoBehaviour {

    [SerializeField]
    private GameObject _elementPrefab = null;
    [SerializeField] 
    private Transform _content = null ;



	// Update is called once per frame
	public void AddElement () {

        GameObject instance = Instantiate(_elementPrefab);
        instance.transform.SetParent(_content);

    }
}
