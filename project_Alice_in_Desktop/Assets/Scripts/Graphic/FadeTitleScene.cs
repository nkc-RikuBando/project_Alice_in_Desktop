using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTitleScene : MonoBehaviour
{
    public void StartFade()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger("Fade");
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
