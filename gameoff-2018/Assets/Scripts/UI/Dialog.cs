using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    // The box containing text
	public GameObject textContainer;

	public Text text;

	// Use this for initialization
	void Start () {
        textContainer.SetActive(false);
	}
	
    void Show() {
        textContainer.SetActive(true);
    }

    void Hide() {
        textContainer.SetActive(false);
    }

    public void Print(string _text) {
        text.text = _text;
    }
}
