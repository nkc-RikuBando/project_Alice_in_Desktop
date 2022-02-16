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
            if (isBreak == false)
            {
                RayCastHit();
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

        /// <summary>
        /// プレイヤーが入って来た
        /// </summary>
        void PlayerEnter()
        {
            stayFlg = true; // 滞在中
            _IHitPlayer.IsHitPlayer();
        }

        /// <summary>
        /// プレイヤーが出て行った
        /// </summary>
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
                    //AudioManager.Instance.SeAction("箱破壊_1");
                    this.StartCoroutine(KeyAppTime());
                }
            }
            /*else */if (UpInput()) playerStatusManager.PlayerIsInput(true);
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、キーを長押し
        /// </summary>
        /// <returns></returns>
        //bool IsStay()
        //{
        //    return stayFlg == true/* && _IActionKey.ActionKey()*/;
        //}

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

        void RayCastHit()
        {
            // Rayの位置の調整値
            Vector3 offset = new Vector3(-2f, 1, 0);
            Vector3 offset2 = new Vector3(0, 1.5f, 0);

            //  Rayの作成　　　　　　　Rayを飛ばす原点　　　Rayを飛ばす方向
            Ray2D horiRay = new Ray2D(transform.position + offset, Vector3.right);
            Ray2D upRay = new Ray2D(transform.position + offset2, Vector3.up);

            // Rayが当たったオブジェクトの情報を入れる箱
            //RaycastHit2D hit;

            // Rayの飛ばせる距離
            int horiRayDis = 4;
            int upRayDis = 2;

            // Rayの可視化    Rayの原点　　　　      Rayの方向　　　       Rayの色
            Debug.DrawRay(horiRay.origin, horiRay.direction * horiRayDis, Color.red);
            Debug.DrawRay(upRay.origin,   upRay.direction * upRayDis,     Color.red);

            //                                   ↓Ray  ↓Rayが当たったオブジェクト ↓距離
            bool horiRayHit = Physics2D.Raycast(horiRay.origin, horiRay.direction, horiRayDis, layer);
            bool upRayHit   = Physics2D.Raycast(upRay.origin,   upRay.direction,   upRayDis,   layer);

            //もしRayにオブジェクトが触れたら
            bool isPlayerHit = horiRayHit || upRayHit;
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
