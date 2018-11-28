using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public World world;
	public GameObject questionContainer;
    public GameObject ans0, ans1;

	private Text questionText;
	private Text ans0Text;
	private Text ans1Text;

    public const float defaultPrintSpeed = 0.05f;

    // Set to true to print the output in Update()
    [HideInInspector]
    public bool isPrinting = false;
    [HideInInspector]
    public Question question;

    private float elapsedTime = 0;
    private string stringBuffer = "";
    private int stringCounter = 0;
    private float printSpeed;

	// Use this for initialization
	void Start () {
        //textContainer.SetActive(false);
        questionText = questionContainer.transform.GetChild(0).GetComponent<Text>();
        ans0Text = ans0.transform.GetChild(0).GetComponent<Text>();
        ans1Text = ans1.transform.GetChild(0).GetComponent<Text>();
	}
	
    void Update() {
        if(isPrinting) {
            if(elapsedTime > printSpeed) {
                if(stringCounter < stringBuffer.Length) {
                    questionText.text += stringBuffer[stringCounter];
                    elapsedTime = 0;
                    stringCounter++;
                }
                else{
                    isPrinting = false;
                    ShowAnswers();
                }
            }
            elapsedTime += Time.unscaledDeltaTime;
        }
    }

    public void Show(Question question, float printSpeed = defaultPrintSpeed) {
        this.question = question;
        questionText.text = "";
        isPrinting = true;
        stringBuffer = question.question;
        stringCounter = 0;
        this.printSpeed = printSpeed;
        questionContainer.SetActive(true);
    }

    public void Hide() {
        isPrinting = false;
        stringCounter = 0;
        stringBuffer = "";
        ans0.SetActive(false);
        ans1.SetActive(false);
        questionContainer.SetActive(false);
    }

    public void ShowAnswers(){
        ans0Text.text = this.question.answers[0].text;
        ans1Text.text = this.question.answers[1].text;
        ans0.SetActive(true);
        ans1.SetActive(true);
    }

    public void Answer(int id) {
        Hide();
        char nextRoom = this.question.answers[id].dst[0];
        bool shouldSpawnDoors = (nextRoom == 'F' ? false : true);
        world.ChangeRoom(nextRoom, shouldSpawnDoors);
    }
}
