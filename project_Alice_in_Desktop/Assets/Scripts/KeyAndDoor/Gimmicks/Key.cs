using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class Key : MonoBehaviour
    {
        private GameObject player; // プレイヤーを保存
        [SerializeField] private string playerName; // プレイヤーの名前を取得
        [SerializeField] private string clearFlgName; // クリアフラグの名前を取得

        private GameObject goal;
        private IGetKey iGetter;

        //private AudioSource se;
        //public AudioClip seClip;
        //[SerializeField] private string seName;

        void Awake()
        {
            player = GameObject.Find(playerName); // プレイヤーオブジェクトを取得
            goal = GameObject.Find(clearFlgName); // ゴールオブジェクトを取得
            iGetter = goal.GetComponent<IGetKey>();
            iGetter.AddKey(gameObject);

            //se = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                //AudioManager.Instance.SeAction(seName);
                //se.PlayOneShot(seClip);
                iGetter.GetKey(gameObject);
            }
        }
    }
}
