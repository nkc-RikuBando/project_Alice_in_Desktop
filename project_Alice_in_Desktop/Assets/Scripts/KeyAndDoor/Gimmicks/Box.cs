using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Inputer;
using GameSystem;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private GameObject player; // プレイヤーを保存
        private ITestKey _ITestKey; // 入力インターフェースを取得
        [SerializeField] private GameObject hideKey; // 鍵を取得
        private bool stayFlg = false;
        //private float inputTime = default;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>(); // 入力インターフェースを取得
            hideKey.SetActive(false); // 鍵を非表示
        }

        void Update()
        {
            if (StayInput())
            {
                //inputTime += 1f * Time.deltaTime;
                if (/*inputTime >= 0.8f*/WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    KeyApp();
                    hideKey.transform.parent = null; // 鍵を子オブジェクトから外す
                    Destroy(gameObject);
                }
            }
            //else
            //    inputTime = default; // キー入力時間を０にする
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
                stayFlg = true; // 滞在中
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
                stayFlg = false; // 滞在してない
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、キーを長押し
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventNagaoshiKey();
        }

        /// <summary>
        /// 鍵が出現する
        /// </summary>
        public void KeyApp()
        {
            hideKey.SetActive(true); //壊れたら鍵が出現
        }
    }
}
