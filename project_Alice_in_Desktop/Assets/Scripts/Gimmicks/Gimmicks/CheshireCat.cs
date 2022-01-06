using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
    // 頑張っててえらい！！！！！！
{
    public class CheshireCat : MonoBehaviour
    {
        private GameObject player; // プレイヤーを保存
        private Animator playerAnim; // プレイヤーのアニメーション保存
        private IPlayerAction _ActionKey;
        private Animator myAnimator;
        private IRenderingFlgSettable iRenderingFlgSettable;
        private bool InOutFlg;

        [Header("ワープ先のオブジェクト")]
        [SerializeField] private GameObject warpPoint; // ワープ先オブジェクトを取得
        private Animator warpPointAnim;                // ワープ先のアニメーション保存

        private bool stayFlg = false;                  // 滞在しているかフラグ
        [Header("はいるUIをアタッチ")]
        [SerializeField] private GameObject hairuUI;

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            playerAnim = player.GetComponent<Animator>();
            _ActionKey = player.GetComponent<IPlayerAction>();
            myAnimator = GetComponent<Animator>();
            iRenderingFlgSettable = GetComponent<IRenderingFlgSettable>();
            InOutFlg = true;
            warpPointAnim = warpPoint.GetComponent<Animator>();
            hairuUI.SetActive(false);
        }

        void Update()
        {
            Warping();
            //if(InOutFlg == false)
            //{

            //}
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
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

        IEnumerator WarpTime()
        {
            yield return new WaitForSeconds(0.375f);
            player.transform.position = warpPoint.transform.position;
        }
    }
}
