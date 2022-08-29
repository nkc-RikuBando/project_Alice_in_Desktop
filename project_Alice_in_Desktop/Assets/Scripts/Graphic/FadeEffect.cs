using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameSystem;

public class FadeEffect : MonoBehaviour
{

    [SerializeField] private AudioClip se; 
    private GameObject player;  // プレイヤーオブジェクト

    private Animator fadeAnimator;
    private AudioSource audioSource;
    private RectTransform clearMaskRect;
    private RectTransform crushingMaskRect;

    private WindowFade windowFade;

    private int waitTime = 2;
    const float SHIFT_Y = -1;

    private string nextSceneName;


    void Start()
    {
        player = GetGameObject.playerObject;
        fadeAnimator = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        clearMaskRect = GameObject.Find("UnMask_Clear").GetComponent<RectTransform>();
        crushingMaskRect = GameObject.Find("UnMask_Crushing").GetComponent<RectTransform>();
        windowFade = GameObject.Find("Camera").GetComponent<WindowFade>();
    }

    public void StartClearEffect()
    {
        clearMaskRect.position = new Vector2(player.transform.position.x, player.transform.position.y);
        PlayAnimation("StageClear");
        waitTime = 2;
        StartCoroutine("Wait");
    }

    public void StartCrushingEffect()
    {
        crushingMaskRect.position = new Vector2(player.transform.position.x, player.transform.position.y + SHIFT_Y);
        audioSource.PlayOneShot(se);
        PlayAnimation("Crushing");
        nextSceneName = SceneManager.GetActiveScene().name;
        waitTime = 2;
        StartCoroutine("Wait");
    }

    public void StartOutsideEffect()
    {
        audioSource.PlayOneShot(se);
        fadeAnimator.SetTrigger("Outside");
        nextSceneName = SceneManager.GetActiveScene().name;
        waitTime = 1;
        StartCoroutine("Wait");
    }

    private void PlayAnimation(string TriggerName)
    {
        fadeAnimator.SetTrigger(TriggerName);
        player.GetComponent<Animator>().SetTrigger(TriggerName);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        windowFade.WindowFadeOut(nextSceneName);
    }
}
