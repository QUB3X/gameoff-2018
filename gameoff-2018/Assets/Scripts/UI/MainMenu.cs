using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Use this function in the editor
    public void DidPressStartButton() {
        Debug.Log("Loading Level #1");
        SceneManager.LoadScene(1);
    }
}
