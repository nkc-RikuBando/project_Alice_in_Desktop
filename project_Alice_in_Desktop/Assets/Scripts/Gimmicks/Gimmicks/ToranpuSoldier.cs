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

        private bool switchFlg;
        private bool tranpuRed = true;

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            animator = GetComponent<Animator>(); // アニメーターを取得
            boxCol = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player && tranpuRed == true)
            {
                animator.SetBool("Defend", true);
                if (Direction())
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else transform.localScale = new Vector3(1, 1, 1);
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
                tranpuRed = false;
                animator.SetBool("Black", true);
            }
            else
            {
                boxCol.enabled = true;
                tranpuRed = true;
                animator.SetBool("Black", false);
            }
        }

        bool Direction()
        {
            return transform.position.x <= player.transform.position.x;
        }
    }
}
