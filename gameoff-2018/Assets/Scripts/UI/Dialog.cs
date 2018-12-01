using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public World world;
    public GameObject questionContainer;
    public GameObject ans0, ans1;
    public Animator animator;

    private Text printedText;
    private Text ans0Text, ans1Text;

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
    private bool skippable;

    // Use this for initialization
    void Start () {
        if (ans0 != null && ans1 != null) {
            printedText = questionContainer.transform.GetChild(0).GetComponent<Text>();
            ans0Text = ans0.transform.GetChild(0).GetComponent<Text>();
            ans1Text = ans1.transform.GetChild(0).GetComponent<Text>();
        }
    }

    void Update() {
        if(isPrinting) {
            if(elapsedTime > printSpeed) {
                if(stringCounter < stringBuffer.Length) {
                    printedText.text += stringBuffer[stringCounter];
                    elapsedTime = 0;
                    stringCounter++;
                }
                else{
                    ConcludeDialog();
                }
            }
            elapsedTime += Time.unscaledDeltaTime;

            // Skip dialog
            if(skippable && Input.anyKeyDown) {
                printedText.text = stringBuffer;

                ConcludeDialog();
            }
        }
    }

    public void ShowQuestion(Question question, float printSpeed = defaultPrintSpeed, bool skippable = true) {
        this.question = question;
        printedText.text = "";
        isPrinting = true;
        stringBuffer = question.question;
        stringCounter = 0;
        this.printSpeed = printSpeed;
        this.skippable = skippable;
        animator.SetTrigger("QuestionIn");
    }

    public void HideQuestion() {
        isPrinting = false;
        stringCounter = 0;
        stringBuffer = "";
        animator.SetTrigger("QuestionOut");
    }

    public void ShowAnswers(){
        ans0Text.text = this.question.answers[0].text;
        ans1Text.text = this.question.answers[1].text;
        animator.SetTrigger("AnswersIn");
    }

    public void Answer(int id) {
        animator.SetTrigger("QuestionOut");
        HideQuestion();
        char nextRoom = this.question.answers[id].dst[0];
        bool shouldSpawnDoors = (nextRoom == 'F' ? false : true);
        world.ChangeRoom(nextRoom, shouldSpawnDoors);
    }

    public void ShowText(Text textbox, string message, float printSpeed = defaultPrintSpeed, bool skippable = true) {
        this.printedText = textbox;
        printedText.text = "";
        isPrinting = true;
        stringBuffer = message;
        stringCounter = 0;

        this.printSpeed = printSpeed;
        this.skippable = skippable;
    }

    private void ConcludeDialog() {
        isPrinting = false;
        if (ans0 != null && ans1 != null) {
            ShowAnswers();
        }
    }
}
