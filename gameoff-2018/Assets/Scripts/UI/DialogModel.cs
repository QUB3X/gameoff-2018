using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Answer
{
    public string text;
    public string dst;
}

[System.Serializable]
public class Question
{
    public string question;
    public Answer[] answers;
}

[System.Serializable]
public class DialogModel
{
    public string welcome;
    public Question[] questions;

    public Question PickRandom(){
        System.Random rand = new System.Random();
        return questions[rand.Next(questions.Length)];
    }
}
