using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class CheshireCat : MonoBehaviour
    {
        private GameObject player;   // プレイヤーを保存
        private Animator playerAnim; // プレイヤーのアニメーション保存
        private IPlayerAction _ActionKey;
        private Animator myAnimator;
        private LayerChange layerChange;
        private CatFrontObj catFrontObj;

        [Header("ワープ先のオブジェクト")]
        [SerializeField] private GameObject warpPoint; // ワープ先オブジェクトを取得
        private CheshireCat warpScr;
        private Animator warpPointAnim;                // ワープ先のアニメーション保存
        private bool warpFlg;
        private const float playerWarpWaitTime = 0.375f;

        private bool stayFlg = false;                  // 滞在しているかフラグ
        [Header("はいるUIをアタッチ")]
        [SerializeField] private GameObject hairuUI;

        //[Range(1, 5)]
        //[SerializeField] private int seNum = 1;

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            playerAnim = player.GetComponent<Animator>();
            _ActionKey = player.GetComponent<IPlayerAction>();
            myAnimator = GetComponent<Animator>();
            layerChange = GetComponent<LayerChange>();
            catFrontObj = GetComponent<CatFrontObj>();
            hairuUI.SetActive(false);
            warpPointAnim = warpPoint.GetComponent<Animator>();
            warpScr = warpPoint.GetComponent<CheshireCat>(); // ワープ先の猫の処理を取得
        }

        void Update()
        {
            Warping();
            WarpValidSwitch();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            bool enterObj = collision.gameObject.tag == "Gimmick" || collision.gameObject.layer == 6;
            // プレイヤーが入って来たら
            if (enterObj)
            {
                catFrontObj.IsFrontObj = true;
                warpScr.catFrontObj.IsFrontObj = true;
                catFrontObj.EnterObjNum += 1;
                warpScr.catFrontObj.EnterObjNum += 1;
            }
            else if (collision.gameObject == player && warpFlg == true)
            {
                stayFlg = true; // 滞在フラグをtrue
                hairuUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            bool exitObj = collision.gameObject.tag == "Gimmick" || collision.gameObject.layer == 6;
            // プレイヤーが出て行ったら
            if (exitObj)
            {
                catFrontObj.IsFrontObj = false;
                warpScr.catFrontObj.IsFrontObj = false;
                catFrontObj.EnterObjNum -= 1;
                warpScr.catFrontObj.EnterObjNum -= 1;
            }
            else if (collision.gameObject == player/* && warpFlg == false*/)
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
            AudioManager.Instance.SeAction("チェシャ猫_1");
            this.StartCoroutine(PlayerWarpStart());
        }

        void WarpValidSwitch()
        {
            bool isDisplayHide = layerChange.OutFlg == true || warpScr.layerChange.OutFlg == true || catFrontObj.IsFrontObj == true || warpScr.catFrontObj.IsFrontObj == true;
            bool enterObjCount = catFrontObj.EnterObjNum != 0 || warpScr.catFrontObj.EnterObjNum != 0;
            if (isDisplayHide ||  enterObjCount) // 閉まっている
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
            yield return new WaitForSeconds(playerWarpWaitTime);
            player.transform.position = warpPoint.transform.position;
        }
    }
}
