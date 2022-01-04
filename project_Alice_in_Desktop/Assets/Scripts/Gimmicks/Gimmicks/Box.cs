using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        private GameObject player; // プレイヤーを保存
        private IPlayerAction _IActionKey;                 // 入力インターフェースを保存
        private IHitPlayer _IHitPlayer;             // 当たり判定インターフェースを保存
        private GameObject hideKey; // 鍵を取得
        private bool stayFlg = false;
        private Animator myAnimator;
        private Animator keyAnimator;

        [SerializeField] private GameObject uiGauge;
        [SerializeField] private float time;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // 入力インターフェースを取得
            _IHitPlayer = uiGauge.GetComponentInChildren<IHitPlayer>(); // 当たり判定インターフェースを取得
            hideKey = GetGameObject.KeyObj;
            hideKey.SetActive(false); // 鍵を非表示
            myAnimator = GetComponent<Animator>();
            keyAnimator = hideKey.GetComponent<Animator>();
            uiGauge.SetActive(false);
        }

        void Update()
        {
            if (StayInput())
            {
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    myAnimator.SetTrigger("Destroy");
                    hideKey.transform.parent = null; // 鍵を子オブジェクトから外す
                    Destroy(uiGauge);
                    this.StartCoroutine(KeyAppTime());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
            {
                stayFlg = true; // 滞在中
                //uiGauge.SetActive(true);
                _IHitPlayer.IsHitPlayer();
            }   
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false; // 滞在してない
                //uiGauge.SetActive(false);
                _IHitPlayer.NonHitPlayer();
            }   
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
        /// 鍵が出現する
        /// </summary>
        public void KeyApp()
        {
            hideKey.SetActive(true); //壊れたら鍵が出現
            hideKey.transform.parent = null; // 鍵を子オブジェクトから外す
            keyAnimator.SetTrigger("Spawn");
        }

        IEnumerator KeyAppTime()
        {
            yield return new WaitForSeconds(time);
            hideKey.SetActive(true); //壊れたら鍵が出現
            keyAnimator.SetTrigger("Spawn");
        }
    }
}
