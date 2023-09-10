using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Operations { add, sub, mul, div }
public enum Line { down, across }
public enum LevelState { notAnswered, answered }

public class LevelManager : MonoBehaviour
{
    [Header("All Questions")]
    public List<Questions> question;

    public delegate void _Completed();
    public static event _Completed LevelCompleted;

    public bool levelComplete;
    public GameObject[] stars;

    public GameObject levelCompleteMessage;

    public float MaxTimer;
    public float MaxBarValue;

    private int currentTime;
    
    public static float timer;
    

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timer = MaxTimer;

        foreach (var q in question)
        {
            q.DisplayQuestion();
            q.OnCorrectAnswer += ResetTime;
            //q.trigger.AddListener(UpdateAllQuestions);
        }
        timer = MaxTimer == 0 ? 60 : MaxTimer;

        LevelCompleted += DisplayPanel;
    }
    private void Update()
    {
        UpdateAllQuestions();
        if (levelComplete) { LevelCompleted?.Invoke(); }
        levelComplete = IsAllComplete();

        //Update Timer
        TimerClock();

    }

    void TimerClock()
    {
        if(timer >= 0)
        {
            timer -= 1 * Time.deltaTime;
            //Debug.Log(timer);
            currentTime = (int)timer;
        }
        else
        {
            return;
        }
    }

    void UpdateAllQuestions()
    {
        foreach (var q in question)
        {
            q.UpdateQuestions();
        }
    }
    bool IsAllComplete()
    {
        foreach (var q in question)
        {
            if (!q.isCorrect)
            {
                return false;
            }
        }
        return true;
    }

    void ResetTime()
    {
        timer = MaxTimer == 0 ? 60 : MaxTimer;

        foreach (var q in question)
        {
            if (q.isCorrect)
            {
                q.OnCorrectAnswer -= ResetTime;
            }
        }

    }

    private void DisplayPanel()
    {
        levelCompleteMessage.SetActive(true);
        LevelCompleted -= DisplayPanel;
    }



}
