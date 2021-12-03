using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Inputer;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        private GameObject player; // プレイヤーを保存
        private ITestKey _ITestKey;
        [SerializeField] private GameObject hideKey; // 鍵を取得
        private bool stayFlg;
        private float inputTime = 0;
        //private bool hitFlg = false;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            player = GameObject.Find("PlayerTest"); // プレイヤーの取得
            _ITestKey = GetComponent<ITestKey>();
            hideKey.SetActive(false);
            stayFlg = false;
        }

        void Update()
        {
            if(StayInput())
            {
                inputTime += 1f * Time.deltaTime;
                if(inputTime >= 3)
                {
                    KeyApp();
                    hideKey.transform.parent = null;
                    Destroy(gameObject);
                }
            }
            else
            {
                inputTime = default; // キー入力時間を０にする
            }
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
            {
                stayFlg = true; // 滞在中
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false; // 滞在してない
            }
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、Qキーを長押し
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventNagaoshiKey();
        }

        /// <summary>
        /// 鍵が出現する
        /// </summary>
        public void KeyApp() // 箱(自分が壊れる)
        {
            hideKey.SetActive(true); //壊れたら鍵が出現
        }
    }
}
