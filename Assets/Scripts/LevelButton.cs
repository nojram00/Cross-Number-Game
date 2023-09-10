using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ButtonState
{
    locked, unlocked
}

public class LevelButton : MonoBehaviour
{
    public Sprite onClickBtn;
    public Sprite lockedIcon;
    public Sprite defaultSprite;
    public SpriteRenderer thisSprite;
    public string SceneName;
    public ButtonState btnState;

    public int levelToUnlocked;
    public int currentLvl;

    private void Start()
    {
        currentLvl = FindObjectOfType<GameMapManager>().currentLevel;

        if(currentLvl >= levelToUnlocked)
        {
            btnState = ButtonState.unlocked;
        }

        thisSprite = GetComponent<SpriteRenderer>();
        if (btnState == ButtonState.locked)
        {
            thisSprite.sprite = lockedIcon;
        }
        defaultSprite = thisSprite.sprite;
    }

    private void OnMouseDown()
    {
        if (btnState == ButtonState.locked) return;
        thisSprite.sprite = onClickBtn;
        thisSprite.color = Color.white;
        if(SceneName != null || SceneName == "")
        {
           StartCoroutine(LoadTheScene());
        }
    }

    private void OnMouseUp()
    {
        if (btnState == ButtonState.locked) return;
        thisSprite.sprite = defaultSprite;
    }

    private void OnMouseEnter()
    {
        if (btnState == ButtonState.locked) return;
        thisSprite.color = Color.gray;
        Debug.Log("Mouse Enter");
    }

    private void OnMouseExit()
    {
        if (btnState == ButtonState.locked) return;
        thisSprite.color = Color.white;
        Debug.Log("Mouse Exit");
    }

    public IEnumerator LoadTheScene()
    {
        var loadasync = SceneManager.LoadSceneAsync(SceneName);

        while (loadasync.progress < 90f)
        {
            Debug.Log("Loading: " + loadasync.progress);
            yield return null;
        }
    }
}
