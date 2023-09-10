using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[Serializable]
public class Questions
{
    [Header("The Question")]
    public List<TMP_InputField> textBox;
    public TMP_Text questionDisplay;

    public delegate void QuestionTrigger();
    public event QuestionTrigger OnCorrectAnswer;
    public float points;

    public bool alreadyAdd = false;
    public int correctAnswer;
    public string question;
    public string inputAnswer;
    public int yourAnswerInInt;

    public bool isCorrect;

    int UpdateText()
    {
        var intAnswer = "";
        foreach(var t in textBox)
        {
            intAnswer += t.text.ToString();
        }

        if(int.TryParse(intAnswer, out var answer))
        {
            return answer;
        }
        else
        {
            return 0;
        }
    }

    void CheckAns()
    {
        if (!IsAllBoxFilled()) return;
        if (isCorrect)
        {
            DisableTextBox();
            OnCorrectAnswer?.Invoke();
            //GameManager.ClearedTextGroup += AddPoints;
            return;
        }
        else
        {
            ResetTextBox();
            return;
        }
    }
    //Check if all boxes are filled:
    bool IsAllBoxFilled()
    {
        foreach(var t in textBox)
        {
            if(t.text.ToString() == "" || t.text.ToString() == " ")
            {
                return false;
            }
        }
        return true;
    }

    bool ValidateAnswer()
    {
        if(yourAnswerInInt == correctAnswer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Disable if Answer is Correct
    async void DisableTextBox()
    {
        await Task.Delay(1000);
        foreach(var t in textBox)
        {
            if (t.interactable == true)
            {
                t.interactable = false;
            }
        }
    }

    //Reset if Wrong
    async void ResetTextBox()
    {
        await Task.Delay(1000);

        foreach (var t in textBox)
        {
            if (t.interactable == true)
            {
                t.text = string.Empty;
            }
        }
    }
    void ChangeTextColor()
    {
        if (isCorrect)
        {
            questionDisplay.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            questionDisplay.fontStyle = FontStyles.Normal;
        }
    }

    public void UpdateQuestions()
    {
        yourAnswerInInt = UpdateText();
        isCorrect = ValidateAnswer();
        CheckAns();
        ChangeTextColor();
    }
    public void DisplayQuestion()
    {
        if (questionDisplay != null)
            questionDisplay.text = question;
    }
}
