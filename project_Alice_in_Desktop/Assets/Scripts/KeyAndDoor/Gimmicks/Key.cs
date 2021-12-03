using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Gimmicks
{
    public class Key : MonoBehaviour
    {
        private GameObject player; // プレイヤーを保存

        private GameObject goal;
        private IGetKey iGetter;

        //private AudioSource se;
        //public AudioClip seClip;

        void Awake()
        {
            player = GameObject.Find("PlayerTest"); // プレイヤーオブジェクトを取得
            goal = GameObject.Find("ClearFlg"); // ゴールオブジェクトを取得
            iGetter = goal.GetComponent<IGetKey>();
            iGetter.AddKey(gameObject);

            //se = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // var tmp = collision.GetComponent<PlayerState>();

            if (collision.gameObject == player)
            {
                //AudioManager.Instance.SeAction("「ハウッ！」");
                //se.PlayOneShot(seClip);
                iGetter.GetKey(gameObject);
            }

            //if (tmp != null)
            //{
            //    iGetter.GetKey(gameObject);
            //}
            //else 
            //{
            //}
        }
    }
}
