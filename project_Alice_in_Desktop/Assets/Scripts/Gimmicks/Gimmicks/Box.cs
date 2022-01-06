using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Player;
using GameSystem;
using Player;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        private GameObject player;                   // プレイヤーを保存
        private IPlayerAction _IActionKey;           // 入力インターフェースを保存
        private PlayerStatusManager playerStatusManager;
        private IHitPlayer _IHitPlayer;              // 当たり判定インターフェースを保存
        [SerializeField] private GameObject hideKey; // 鍵を取得
        private bool stayFlg = false;
        private Animator myAnimator;                 // 箱(自身)のアニメーションを保存
        private Animator keyAnimator;                // 鍵のアニメーションを保存

        [SerializeField] private GameObject uiGauge; // ゲージを保存
        [SerializeField] private float time;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            // 入力インターフェースを取得
            _IActionKey = player.GetComponent<IPlayerAction>();

            // 当たり判定インターフェースを取得
            _IHitPlayer = uiGauge.GetComponentInChildren<IHitPlayer>();
            playerStatusManager = player.GetComponent<PlayerStatusManager>();
            //hideKey = GetGameObject.KeyObj;
            hideKey.SetActive(false);              // 鍵を非表示
            myAnimator = GetComponent<Animator>(); // 箱(自身)のアニメーションを取得
            keyAnimator = hideKey.GetComponent<Animator>(); // 鍵のアニメーションを取得
            uiGauge.SetActive(false);              // ゲージを非表示
        }

        void Update()
        {
            BoxBreak();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
            {
                stayFlg = true; // 滞在中
                _IHitPlayer.IsHitPlayer();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false; // 滞在してない
                _IHitPlayer.NonHitPlayer();
            }
        }


        /// <summary>
        /// 箱(自身)が壊れる
        /// </summary>
        void BoxBreak()
        {
            if (StayInput())
            {
                playerStatusManager.PlayerIsInput(false);
                // ゲージが溜まったら
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    myAnimator.SetTrigger("Destroy");  // アニメーション再生
                    hideKey.transform.parent = null;   // 鍵を子オブジェクトから外す
                    uiGauge.SetActive(false);          // ゲージを一旦隠す
                    this.StartCoroutine(KeyAppTime());
                }
            }
            else if(UpInput())playerStatusManager.PlayerIsInput(true);
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、キーを長押し
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _IActionKey.ActionKey();
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、キーを離す
        /// </summary>
        /// <returns></returns>

        bool UpInput() 
        {
            return stayFlg == true && _IActionKey.ActionKeyUp();
        }

        /// <summary>
        /// 鍵が出現する
        /// </summary>
        public void KeyApp()
        {
            hideKey.SetActive(true); //壊れたら鍵が出現
            hideKey.transform.parent = null; // 鍵を子オブジェクトから外す
            keyAnimator.SetTrigger("Spawn");
        }

        /// <summary>
        /// 鍵が出現する
        /// </summary>
        /// <returns></returns>
        IEnumerator KeyAppTime()
        {
            yield return new WaitForSeconds(time);
            hideKey.SetActive(true); //壊れたら鍵が出現
            keyAnimator.SetTrigger("Spawn");
        }
    }
}
