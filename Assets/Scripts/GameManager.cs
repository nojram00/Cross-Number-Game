using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
 
    public float increase;
    public Slider powerBar;
    public float PowerScore;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }      
    }
    private void Start()
    {
        foreach (var q in LevelManager.Instance.question)
        {
            q.OnCorrectAnswer += IncreasePowerBar;
        }

        if(GameObject.Find("Bar").TryGetComponent(out Slider sliderBar))
        {
            powerBar = sliderBar;
            powerBar.maxValue = LevelManager.Instance == null ? 100 : LevelManager.Instance.MaxBarValue;
        }
    }


    public void IncreasePowerBar()
    {
        PowerScore += increase * LevelManager.timer;
        Debug.Log("LOLS!");
        foreach (var q in LevelManager.Instance.question)
        {
            if (q.isCorrect)
            {
                q.OnCorrectAnswer -= IncreasePowerBar;
            }
        }
    }
    private void Update()
    {
        powerBar.value = PowerScore;
        //ClearedTextGroup?.Invoke();
    }
}
