using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour {

    [HideInInspector]
    public Dialog dialog;

    public Text introText;
    // Use this for initialization
    private bool didStartPrintingText = false;
    void Start () {
        // Use this to init all the dialog lines contained in Resources/dialogs.txt
        TextAsset jsonString = Resources.Load("dialogs") as TextAsset;
        DialogModel dialogs = JsonUtility.FromJson<DialogModel>(jsonString.text);
        dialog = GetComponent<Dialog>();
        dialog.ShowWelcome(introText, dialogs.welcome);
        didStartPrintingText = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(!dialog.isPrinting && didStartPrintingText) {
            SceneManager.LoadScene("playground");
        }
	}
}
