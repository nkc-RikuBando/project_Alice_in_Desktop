using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
// 頑張っててえらい！！！！！！
{
    public class CheshireCat : MonoBehaviour/*, IRenderingFlgSettable*/
    {
        private GameObject player; // プレイヤーを保存
        private Animator playerAnim; // プレイヤーのアニメーション保存
        private IPlayerAction _ActionKey;
        private Animator myAnimator;
        //private bool InOutFlg;                         // 画面外にいるか
        private LayerChange layerChange;

        [Header("ワープ先のオブジェクト")]
        [SerializeField] private GameObject warpPoint; // ワープ先オブジェクトを取得
        private CheshireCat warpScr;
        private Animator warpPointAnim;                // ワープ先のアニメーション保存
        private bool warpFlg;
        private const float plaerWarpWaitTime = 0.375f;

        private bool stayFlg = false;                  // 滞在しているかフラグ
        [Header("はいるUIをアタッチ")]
        [SerializeField] private GameObject hairuUI;

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            playerAnim = player.GetComponent<Animator>();
            _ActionKey = player.GetComponent<IPlayerAction>();
            myAnimator = GetComponent<Animator>();
            //InOutFlg = true;
            layerChange = GetComponent<LayerChange>();
            warpPointAnim = warpPoint.GetComponent<Animator>();
            hairuUI.SetActive(false);
            warpScr = warpPoint.GetComponent<CheshireCat>();
        }

        void Update()
        {
            Warping();
            WarpValidSwitch();
            Debug.Log(warpFlg);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player && layerChange.OutFlg == false)
            {
                stayFlg = true; // 滞在フラグをtrue
                hairuUI.SetActive(true);
            }
            else
            {
                warpFlg = false;
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
            if (StayInput()) PlayerWarpToHandlePos();
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、キーを押す
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ActionKey.ActionKey_Down() && warpFlg == true;
        }

        /// <summary>
        /// ワープ先にプレイヤーを移動させる
        /// </summary>
        void PlayerWarpToHandlePos()
        {
            playerAnim.SetTrigger("Teleport");
            myAnimator.SetTrigger("Teleport");
            warpPointAnim.SetTrigger("Teleport");
            this.StartCoroutine(PlayerWarpStart());
        }

        void WarpValidSwitch()
        {
            bool isDisplayHide = layerChange.OutFlg == true || warpScr.layerChange.OutFlg == true;
            if (isDisplayHide)
            {
                //hairuUI.SetActive(false);
                warpFlg = false;
                myAnimator.SetBool("Close", true);
                warpPointAnim.SetBool("Close", true);
            }
            else
            {
                warpFlg = true;
                myAnimator.SetBool("Close", false);
                warpPointAnim.SetBool("Close", false);
            }
        }

        IEnumerator PlayerWarpStart()
        {
            yield return new WaitForSeconds(plaerWarpWaitTime);
            player.transform.position = warpPoint.transform.position;
        }
    }
}
