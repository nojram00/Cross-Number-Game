using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> AnimationSequence;
    public DialogueManager[] dialogueManager;

    //public delegate void Tuts();
    //public static event Tuts OnCompleted;

    int index;
    private void Start()
    {
        foreach(var q in LevelManager.Instance.question)
        {
            q.OnCorrectAnswer += playAnimation;
        }
        dialogueManager[0].OnDialogueEnd += PlayNextDialogue;
    }

    void playAnimation()
    {
        AnimationSequence[0].SetActive(false);
        AnimationSequence[1].SetActive(true);
        foreach (var q in LevelManager.Instance.question)
        {
            q.OnCorrectAnswer -= playAnimation;
            if (!q.isCorrect)
            {
                q.OnCorrectAnswer += playeAnotherAnimation;
            }
        }
    }

    void playeAnotherAnimation()
    {
        AnimationSequence[1].SetActive(false);
        AnimationSequence[2].SetActive(true);
        foreach (var q in LevelManager.Instance.question)
        {
            q.OnCorrectAnswer -= playeAnotherAnimation;
            if (!q.isCorrect)
            {

            }
        }
    }

    void PlayNextDialogue()
    {
        AddFuckingDelay();
    }
    
    async void AddFuckingDelay()
    {
        await Task.Delay(1000);
        AnimationSequence[3].SetActive(true);
        dialogueManager[1].OnDialogueEnd += PlayTutorial1;
    }
    async void PlayTutorial1()
    {
        dialogueManager[1].OnDialogueEnd -= PlayTutorial1;
        await Task.Delay(1000);
        AnimationSequence[4].SetActive(true);
        dialogueManager[2].OnDialogueEnd += PlayTutorial2;
    }
    async void PlayTutorial2()
    {
        dialogueManager[2].OnDialogueEnd -= PlayTutorial2;
        await Task.Delay(1000);
        AnimationSequence[5].SetActive(true);
    }

}
