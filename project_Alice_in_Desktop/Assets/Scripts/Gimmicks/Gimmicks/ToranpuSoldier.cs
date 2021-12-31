using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class ToranpuSoldier : MonoBehaviour, IHitSwitch
    {
        private GameObject player; // �v���C���[��ۑ�
        private Animator animator; // �A�j���[�^�[��ۑ�
        private BoxCollider2D boxCol;
        //private Vector3 localScale; // �X�P�[����ۑ�
        //private Vector3 pos;        // �ʒu�̕ۑ�

        private bool switchFlg;

        void Start()
        {
            player = GetGameObject.playerObject; // �v���C���[���擾
            animator = GetComponent<Animator>(); // �A�j���[�^�[���擾
            boxCol = GetComponent<BoxCollider2D>();
            //localScale = transform.localScale;   // �X�P�[�����擾
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
