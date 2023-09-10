using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComp;

    [TextArea(3, 10)]
    public string[] dialogueLines;
    public float textSpeed;

    private int index;

    public GameObject AnimationHandler;

    public delegate void DialogueEvent();
    public event DialogueEvent OnDialogueEnd;

    void Start()
    {
        textComp.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComp.text == dialogueLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComp.text = dialogueLines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeEffect());
    }
    void NextLine()
    {
        if(index < dialogueLines.Length - 1) 
        {
            index++;
            textComp.text = string.Empty;
            StartCoroutine(TypeEffect());
        }
        else
        {
            //gameObject.SetActive(false);
            AnimationHandler.SetActive(false);
            OnDialogueEnd?.Invoke();
        }
    }

    IEnumerator TypeEffect()
    {
        foreach(char c in dialogueLines[index].ToCharArray()) 
        {
            textComp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
