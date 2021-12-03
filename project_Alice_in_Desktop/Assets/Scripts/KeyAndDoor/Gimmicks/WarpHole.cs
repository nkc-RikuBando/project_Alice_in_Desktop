using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks
{
    public class WarpHole : MonoBehaviour
    {
        private GameObject player;                     // プレイヤーオブジェクトを保存
        private ITestKey _ITestKey;
        [SerializeField] private GameObject warpPoint; // ワープ先オブジェクトを保存
        private bool stayFlg = false;                  // 滞在しているかフラグ

        void Start()
        {
            player = GameObject.Find("PlayerTest"); // プレイヤーオブジェクトを取得
            _ITestKey = GetComponent<ITestKey>();
        }

        void Update()
        {
            if (StayInput())
            {
                Warp();
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player) stayFlg = true; // 滞在フラグをtrue
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player) stayFlg = false; // 滞在フラグをfalse
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、Qキーを押す
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventKey();
        }

        /// <summary>
        /// ワープ先にプレイヤーを移動させる
        /// </summary>
        void Warp()
        {
            player.transform.position = warpPoint.transform.position;
        }
    }
}
