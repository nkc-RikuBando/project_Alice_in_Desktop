using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Window;


public class CursolAppear : MonoBehaviour,IWindowTouch,IWindowLeave
{
    [SerializeField] private GameObject tutorialObj;
    private GameObject obj;
    [SerializeField] private Vector3 pos;
    [SerializeField] private float appearTime;
    private bool appearFlg = true;
    void Start()
    {
        StartCoroutine(Appear());
    }

    public void DeleteCursol()
    {
        appearFlg = false;
        Destroy(obj);
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(appearTime);
        if (appearFlg)
        {
            obj = Instantiate(tutorialObj);
            obj.transform.position = pos;
        }
    }

    public void WindowTouchAction()
    {
        if (appearFlg == true)
        {
            DeleteCursol();
        }
    }

    public void WindowLeaveAction()
    {
    }
}