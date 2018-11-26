using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    // The box containing text
	public GameObject textContainer;

	public Text text;

    public const float defaultPrintSpeed = 0.3f;

    // Set to true to print the output in Update()
    [HideInInspector]
    public bool isPrinting = false;

    private float elapsedTime = 0;
    private string stringBuffer = "";
    private int stringCounter = 0;
    private float printSpeed;

	// Use this for initialization
	void Start () {
        textContainer.SetActive(false);
	}
	
    void Update() {
        if(isPrinting) {
            if(elapsedTime > printSpeed) {
                if(stringCounter < stringBuffer.Length) {
                    text.text += stringBuffer[stringCounter];
                    elapsedTime = 0;
                    stringCounter++;
                }
            }
            elapsedTime += Time.deltaTime;
        }
    }

    public void Show(string content, float printSpeed = defaultPrintSpeed) {
        textContainer.SetActive(true);
        isPrinting = true;
        text.text = "";
        stringBuffer = content;
        stringCounter = 0;
        this.printSpeed = printSpeed;
    }

    public void Hide() {
        textContainer.SetActive(false);
        isPrinting = false;
        stringCounter = 0;
        stringBuffer = "";
    }
}
