using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Animation;
using Player;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// 鍵を知っている。

        // 鍵のリスト
        [Header("鍵のアタッチ不要")]
        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        private GameObject player;
        private GameObject inputUI;
        private IPlayerAction _IActionKey;        // 入力インターフェースを保存
        private PlayerStatusManager playerStatusManager;
        private Animator animator;                  // アニメーターの保存

        private ClearEffect clearEffect;            // クリアエフェクトの保存
        private bool clearFlg;
        private bool stayFlg;

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // 入力インターフェースを取得
            playerStatusManager = player.GetComponent<PlayerStatusManager>();
            playerStatusManager.PlayerIsInput(true);
            animator = GetComponent<Animator>();  // アニメーターを取得
            clearEffect = GetComponent<ClearEffect>(); // クリアエフェクトを取得
            clearFlg = false;
            stayFlg = false;
            inputUI = GetUIObject.HirakuUI;
            inputUI.SetActive(false);
            if (keyList.Count <= 0) Clear();
        }

        void Update()
        {
            ClearAnime();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                stayFlg = true;
                inputUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                stayFlg = false;
                inputUI.SetActive(false);
            }
        }

        public void AddKey(GameObject set)
        {
            keyList.Add(set);
        }

        // 鍵が消えたらリストから消す
        public void GetKey(GameObject get)
        {
            keyList.Remove(get);             // リストから鍵を消す
            if (keyList.Count <= 0) Clear(); // クリアメソッドを呼ぶ
            //Destroy(get); // リストから消えたら鍵自身を消す
        }

        public void Clear()
        {
            clearFlg = true; // クリアフラグをtrueにする
        }

        /// <summary>
        /// クリア時の演出
        /// </summary>
        void ClearAnime()
        {
            animator.SetBool("Locked", true);
            if (IsSceceMove())
            {
                if (clearFlg == true)
                {
                    playerStatusManager.PlayerIsInput(false);
                    animator.SetTrigger("Action");
                    clearEffect.StartClearEffect();
                }
                else
                    animator.SetTrigger("Action");
            }
            if (clearFlg == true)
                animator.SetBool("Locked", false);
        }

        /// <summary>
        /// シーン移動する条件
        /// </summary>
        /// <returns></returns>
        bool IsSceceMove()
        {
            return _IActionKey.ActionKey_Down() && stayFlg == true;
        }
    }
}
