using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavigator : MonoBehaviour
{
    public Animator transitionAnimator;

    public async void SceneNavigate(string sceneName)
    {
        transitionAnimator.SetTrigger("Navigate");
        await Task.Delay(2000);
        var a = SceneManager.LoadSceneAsync(sceneName);

        while(a.progress < .9f)
        {
            Debug.Log("Loading... " + a.progress * 100 + "%");
        }
    }
}
