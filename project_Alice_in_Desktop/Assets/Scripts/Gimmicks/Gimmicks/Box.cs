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

        [SerializeField] private LayerMask layer;

        private bool isBreak;

        public enum AnimeType
        {
            BOX_BREAK, KEY_APP
        }
        AnimeType animeType;

        //public bool PlHitFlg
        //{
        //    get { return stayFlg; }
        //    set { stayFlg = value; }
        //}

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
            isBreak = false;
        }

        void Update()
        {
            //AnimePlay();
            
            if (isBreak == false)
            {
                //UpRayCast();
                HorizontalRay();
            }
            BoxBreak();
        }

        void AnimePlay()
        {
            switch (animeType)
            {
                case AnimeType.BOX_BREAK:
                    myAnimator.SetTrigger("Destroy");
                    break;
                case AnimeType.KEY_APP:
                    keyAnimator.SetTrigger("Spawn");
                    break;
            }
        }

        void PlayerEnter()
        {
            stayFlg = true; // 滞在中
            _IHitPlayer.IsHitPlayer();
        }

        void PlayerExit()
        {
            stayFlg = false; // 滞在してない
            _IHitPlayer.NonHitPlayer();
        }

        /// <summary>
        /// 箱(自身)が壊れる
        /// </summary>
        void BoxBreak()
        {
            //if (IsStay())
            {
                //playerStatusManager.PlayerIsInput(false);
                // ゲージが溜まったら
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    isBreak = true;
                    WaitTimeUI.gaugeMaxFlg = false;
                    myAnimator.SetTrigger("Destroy");  // アニメーション再生
                    hideKey.transform.parent = null;   // 鍵を子オブジェクトから外す
                    uiGauge.SetActive(false);          // ゲージを一旦隠す
                    this.StartCoroutine(KeyAppTime());
                }
            }
            /*else */if (UpInput()) playerStatusManager.PlayerIsInput(true);
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、キーを長押し
        /// </summary>
        /// <returns></returns>
        bool IsStay()
        {
            return stayFlg == true/* && _IActionKey.ActionKey()*/;
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

        void HorizontalRay()
        {
            // Rayの位置の調整値
            Vector3 offset = new Vector3(-2f, 1, 0);
            Vector3 offset2 = new Vector3(0, 1.5f, 0); // 上

            //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
            Ray2D ray = new Ray2D(transform.position + offset, Vector3.right);
            Ray2D ray2 = new Ray2D(transform.position + offset2, Vector3.up); // 上

            //Rayが当たったオブジェクトの情報を入れる箱
            //RaycastHit2D hit;

            //Rayの飛ばせる距離
            int distance = 4;
            int distance2 = 2; // 上

            //Rayの可視化   ↓Rayの原点　　　　↓Rayの方向　　　↓Rayの色
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            Debug.DrawRay(ray2.origin, ray2.direction * distance2, Color.red); // 上

            //               ↓Ray  ↓Rayが当たったオブジェクト ↓距離
            bool hit = Physics2D.Raycast(ray.origin, ray.direction, distance, layer);
            bool hit2 = Physics2D.Raycast(ray2.origin, ray.direction, distance2, layer);

            //もしRayにオブジェクトが衝突したら
            bool isPlayerHit = hit || hit2;
            if (isPlayerHit) PlayerEnter();
            else PlayerExit();
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
