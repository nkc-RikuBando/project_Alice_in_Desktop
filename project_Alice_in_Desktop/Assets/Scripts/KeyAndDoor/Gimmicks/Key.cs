using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject player;

    private GameObject goal;
    private IGetKey iGetter;

    //private AudioSource se;
    //public AudioClip seClip;

    void Awake()
    {
        //player = PlayerTest.player; // プレイヤーオブジェクトを取得

        goal = GameObject.Find("ClearFlg"); // ゴールオブジェクトを取得
        iGetter = goal.GetComponent<IGetKey>();
        iGetter.AddKey(gameObject);

        //se = GetComponent<AudioSource>();
    }

    void Start()
    {
        player = PlayerTest.player; // プレイヤーオブジェクトを取得
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("YUI");
            //AudioManager.Instance.SeAction("「ハウッ！」");
            //se.PlayOneShot(seClip);
            iGetter.GetKey(gameObject);
        }
    }
}
