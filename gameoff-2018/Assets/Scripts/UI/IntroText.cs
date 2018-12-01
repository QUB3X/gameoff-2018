using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour {

    [HideInInspector]
    public Dialog dialog;

    public Text introText;

    void Start () {
        // Use this to init all the dialog lines contained in Resources/dialogs.txt
        TextAsset jsonString = Resources.Load("dialogs") as TextAsset;
        DialogModel dialogs = JsonUtility.FromJson<DialogModel>(jsonString.text);
        dialog = GetComponent<Dialog>();
        dialog.ShowText(introText, dialogs.welcome, skippable: false);
    }
	
	// Update is called once per frame
    void Update () {
        if(Input.anyKeyDown && !dialog.isPrinting) {
            SceneManager.LoadScene("playground");
        }
    }
}
