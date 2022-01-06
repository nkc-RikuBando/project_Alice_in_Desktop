using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
// 頑張っててえらい！！！！！！
{
    public class CheshireCat : MonoBehaviour, IRenderingFlgSettable
    {
        private GameObject player; // プレイヤーを保存
        private Animator playerAnim; // プレイヤーのアニメーション保存
        private IPlayerAction _ActionKey;
        private Animator myAnimator;
        private bool InOutFlg = true;                         // 画面外にいるか

        [Header("ワープ先のオブジェクト")]
        [SerializeField] private GameObject warpPoint; // ワープ先オブジェクトを取得
        private CheshireCat warpScr;
        private Animator warpPointAnim;                // ワープ先のアニメーション保存

        private bool stayFlg = false;                  // 滞在しているかフラグ
        [Header("はいるUIをアタッチ")]
        [SerializeField] private GameObject hairuUI;

        public bool InOut
        {
            get { return InOutFlg; }
            set { InOutFlg = value; }
        }

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            playerAnim = player.GetComponent<Animator>();
            _ActionKey = player.GetComponent<IPlayerAction>();
            myAnimator = GetComponent<Animator>();
            InOutFlg = true;
            warpScr = warpPoint.GetComponent<CheshireCat>();
            warpPointAnim = warpPoint.GetComponent<Animator>();
            hairuUI.SetActive(false);
        }

        void Update()
        {
            Warping();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player && warpScr.InOut)
            {
                stayFlg = true; // 滞在フラグをtrue
                hairuUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false; // 滞在フラグをfalse
                hairuUI.SetActive(false);
            }
        }

        /// <summary>
        /// ワープする
        /// </summary>
        void Warping()
        {
            if (StayInput()) WarpPlace();
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、Qキーを押す
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ActionKey.ActionKey_Down();
        }

        /// <summary>
        /// ワープ先にプレイヤーを移動させる
        /// </summary>
        void WarpPlace()
        {
            playerAnim.SetTrigger("Teleport");
            myAnimator.SetTrigger("Teleport");
            warpPointAnim.SetTrigger("Teleport");
            this.StartCoroutine(WarpTime());
        }

        public void SetRenderingFlg(bool val)
        {
            InOutFlg = val;
            if (InOutFlg == false)
            {
                warpScr.enabled = false;
                myAnimator.SetBool("Close", true);
                warpPointAnim.SetBool("Close", true);
            }
            else
            {
                warpScr.enabled = true;
                myAnimator.SetBool("Close", false);
                warpPointAnim.SetBool("Close", false);
            }
        }

        IEnumerator WarpTime()
        {
            yield return new WaitForSeconds(0.375f);
            player.transform.position = warpPoint.transform.position;
        }
    }
}
