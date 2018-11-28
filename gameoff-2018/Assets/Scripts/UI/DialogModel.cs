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
    public List<Question> questions;

    public Question PickRandom(){
        System.Random rand = new System.Random();
        int idx = rand.Next(questions.Count);
        Question q = questions[idx];
        questions.RemoveAt(idx);
        return q;
    }
}
