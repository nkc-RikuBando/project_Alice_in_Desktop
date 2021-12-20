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
        private ITestKey _ITestKey;                 // 入力インターフェースを保存
        private IHitPlayer _IHitPlayer;             // 当たり判定インターフェースを保存
        [SerializeField] private GameObject hideKey; // 鍵を取得
        private bool stayFlg = false;
        private Animator animator;
        [SerializeField] private GameObject uiGauge;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>(); // 入力インターフェースを取得
            _IHitPlayer = GetComponent<IHitPlayer>(); // 当たり判定インターフェースを取得
            hideKey.SetActive(false); // 鍵を非表示
            animator = hideKey.GetComponent<Animator>();
            uiGauge.SetActive(false);
        }

        void Update()
        {
            if (StayInput())
            {
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    KeyApp();
                    hideKey.transform.parent = null; // 鍵を子オブジェクトから外す
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
            {
                stayFlg = true; // 滞在中
                uiGauge.SetActive(true);
                _IHitPlayer.IsHitPlayer(stayFlg);
            }   
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false; // 滞在してない
                uiGauge.SetActive(false);
                _IHitPlayer.IsHitPlayer(stayFlg);
            }   
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
            animator.SetTrigger("Spawn");
        }
    }
}
