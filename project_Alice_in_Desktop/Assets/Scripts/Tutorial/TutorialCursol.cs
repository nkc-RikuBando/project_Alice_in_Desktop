using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TutorialCursol : MonoBehaviour
{
    [SerializeField] private float appearTime = 15f;
    [SerializeField] private Sprite defaultCursol;
    [SerializeField] private Sprite horizontalArrowCursol;
    private SpriteRenderer sr;
    void Start()
    {
        sr.GetComponent<SpriteRenderer>();
        StartCoroutine(CursolMove());
    }

    void Update()
    {
    }

    private IEnumerator CursolMove()
    {
        yield return new WaitForSeconds(appearTime);
        sr.sprite = defaultCursol;
        
    }
}
