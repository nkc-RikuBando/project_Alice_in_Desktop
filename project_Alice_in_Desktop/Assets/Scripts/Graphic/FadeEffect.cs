using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeEffect : MonoBehaviour
{
    [SerializeField] private string playerObjectName = "Player";

    private GameObject player;  // プレイヤーオブジェクト

    private Animator fadeAnimator;
    private RectTransform clearMaskRect;
    private RectTransform crushingMaskRect;

    private WindowFade windowFade;

    private int waitTime = 2;
    const float SHIFT_Y = -1;

    private string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerObjectName);
        fadeAnimator = gameObject.GetComponent<Animator>();
        clearMaskRect = GameObject.Find("UnMask_Clear").GetComponent<RectTransform>();
        crushingMaskRect = GameObject.Find("UnMask_Crushing").GetComponent<RectTransform>();
        windowFade = GameObject.Find("Camera").GetComponent<WindowFade>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift)) StartClearEffect();
        //if (Input.GetKeyDown(KeyCode.LeftAlt)) StartCrushingEffect();
        //if (Input.GetKeyDown(KeyCode.LeftControl)) StartOutsideEffect();
    }

    public void StartClearEffect()
    {
        clearMaskRect.position = new Vector2(player.transform.position.x, player.transform.position.y);
        PlayAnimation("StageClear");
        //nextSceneName = "BandoScene_NEO";
        waitTime = 2;
        StartCoroutine("Wait");
    }

    public void StartCrushingEffect()
    {
        crushingMaskRect.position = new Vector2(player.transform.position.x, player.transform.position.y + SHIFT_Y);
        PlayAnimation("Crushing");
        nextSceneName = SceneManager.GetActiveScene().name;
        waitTime = 2;
        StartCoroutine("Wait");
    }

    public void StartOutsideEffect()
    {
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
