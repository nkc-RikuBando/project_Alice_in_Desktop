using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowFade : MonoBehaviour
{
    private Animator cameraAnimator;
    private string nextName = "";

    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift)) WindowFadeOut("BandoScene_NEO");
    }

    // シーン遷移時に呼び出す 引数に遷移先のシーン名
    public void WindowFadeOut(string nextSceneName)
    {
        nextName = nextSceneName;
        cameraAnimator.SetTrigger("Close");
    }

    // アニメーションから呼び出す
    public void SceneChange()
    {
        SceneManager.LoadScene(nextName);
    }
}
