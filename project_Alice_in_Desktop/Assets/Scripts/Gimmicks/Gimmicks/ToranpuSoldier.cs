using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class ToranpuSoldier : MonoBehaviour, IHitSwitch
    {
        private GameObject player; // プレイヤーを保存
        private Animator animator; // アニメーターを保存
        private BoxCollider2D boxCol;
        //private Vector3 localScale; // スケールを保存
        //private Vector3 pos;        // 位置の保存

        private bool switchFlg;

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            animator = GetComponent<Animator>(); // アニメーターを取得
            boxCol = GetComponent<BoxCollider2D>();
            //localScale = transform.localScale;   // スケールを取得
            //pos.x = transform.position.x;
            //stayFlg = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                animator.SetBool("Defend", true);
                //if (Direction())
                //{
                //    localScale = new Vector3(-1, 1, 1);
                //}
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                animator.SetBool("Defend", false);
            }
        }

        public void Switch(bool switchOn)
        {
            switchFlg = switchOn;
            if (switchFlg == true)
            {
                boxCol.enabled = false;
                animator.SetBool("Black", true);
            }
            else
            {
                boxCol.enabled = true;
                animator.SetBool("Black", false);
            }
        }

        bool Direction()
        {
            return transform.position.x <= player.transform.position.x;
        }
    }
}
