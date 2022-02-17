using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    private WindowFade windowFade;


    // Start is called before the first frame update
    void Start()
    {
        windowFade = GameObject.Find("Camera").GetComponent<WindowFade>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade()
    {
        windowFade.WindowFadeOut("SelectScene");
    }

    public void Reload()
    {
        windowFade.WindowFadeOut(SceneManager.GetActiveScene().name);
    }

    public void FadeTitle()
    {
        windowFade.WindowFadeOut("TitleScene");
    }
}
