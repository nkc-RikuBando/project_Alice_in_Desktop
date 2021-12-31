using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.Player;
using Animation;

namespace GameSystem
{
    public class ClearFig2 : MonoBehaviour, IHitSwitch
    {
        [SerializeField] private GameObject player;
        private IPlayerAction _IActionKey;                 // 入力インターフェースを保存
        private Animator animator;                  // アニメーターを保存
        private ClearEffect clearEffect;            // クリアエフェクトを保存

        private bool switchFlg;
        private bool stayFlg;
        //[SerializeField] private string sceneName;   // シーン移動先の名前
        //[SerializeField] private float fadeTime;     // フェードする時間

        public enum AnimeType
        {
            DOOR_ROCK_NOW, DOOR_ROCK_KAIJO, DOOR_ROOK_ACTION
        }
        AnimeType animeType;

        void Start()
        {
            _IActionKey = player.GetComponent<IPlayerAction>(); // 入力インターフェースを取得
            animator = GetComponent<Animator>();  // アニメーターを取得
            clearEffect = GetComponent<ClearEffect>(); // クリアエフェクトを取得
            switchFlg = false;
            stayFlg = false;
        }

        void Update()
        {
            AnimeSwitch();
            AnimePlay();
            Debug.Log("switchは" + switchFlg);

            //animator.SetBool("Locked", true);          // 鍵がかかっているアニメ
            //if(IsSceceMove())
            //{
            //    if (switchFlg == true) 　　　　　　　　// スイッチオン
            //        animator.SetBool("Locked", false); // 鍵解除アニメ
            //    else                   　　　　　　　　// スイッチオフ
            //        animator.SetTrigger("Action");　　 // 鍵がかかっているアクション
            //}
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = false;
        }

        public void Switch(bool switchOn)
        {
            switchFlg = switchOn;
        }

        /// <summary>
        /// アニメーターの種類
        /// </summary>
        void AnimeSwitch()
        {
            switch (animeType)
            {
                case AnimeType.DOOR_ROCK_KAIJO:
                    animator.SetBool("Locked", false);
                    break;
                case AnimeType.DOOR_ROCK_NOW:
                    animator.SetBool("Locked", true);
                    break;
                case AnimeType.DOOR_ROOK_ACTION:
                    animator.SetTrigger("Action");
                    break;
            }
        }

        /// <summary>
        /// アニメーターの実行
        /// </summary>
        void AnimePlay()
        {
            animeType = AnimeType.DOOR_ROCK_NOW;
            if (switchFlg == true)
            {
                animeType = AnimeType.DOOR_ROCK_KAIJO;
            }

            if (IsSceceMove())
            {
                animeType = AnimeType.DOOR_ROOK_ACTION;
                if(switchFlg == true)
                {
                    clearEffect.StartClearEffect();
                    // FadeManager.Instance.LoadScene(sceneName, fadeTime);
                }
            }
        }

        /// <summary>
        /// キー入力する＆プレイヤーが滞在
        /// </summary>
        /// <returns></returns>
        bool IsSceceMove()
        {
            return _IActionKey.ActionKey_Down() && stayFlg == true;
        }
    }
}
