using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class ToranpuSoldier : MonoBehaviour, IHitSwitch/*, ISetSwitch*/
    {
        private GameObject player; // プレイヤーを保存
        private Animator animator; // アニメーターを保存
        [SerializeField] private CapsuleCollider2D capsuleCol;

        [Header("CardSwitchをアタッチ")]
        [SerializeField] private List<GameObject> inputSwitch = new List<GameObject>();
        [SerializeField] private List<ISetToranpuSoldier> iSetSoldier = new List<ISetToranpuSoldier>();

        private bool switchFlg;
        private bool tranpuRed = true;
        [Header("トランプが黒")]
        [SerializeField] private bool blackOn;

        void Awake()
        {
            for (int i = 0; i < inputSwitch.Count; i++)
            {
                inputSwitch[i].GetComponent<IHitSwitch>();
                iSetSoldier.Add(inputSwitch[i].GetComponent<ISetToranpuSoldier>());
                iSetSoldier[i].AddSoldier(gameObject);
            }
        }

        void Start()
        {
            player = GetGameObject.playerObject; // プレイヤーを取得
            animator = GetComponent<Animator>(); // アニメーターを取得

            if (blackOn == true)
            {
                animator.SetBool("Black", true);
                BlackMode();
            }
            else animator.SetBool("Black", false);
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

        public void Switch()
        {
            //switchFlg = switchOn;
            switchFlg = switchFlg ? false : true;
            if(blackOn == false)
            {
                if (switchFlg == true) BlackMode();
                else RedMode();
            }
            else
            {
                if (switchFlg == true) RedMode();
                else BlackMode();
            }
        }

        void BlackMode()
        {
            capsuleCol.enabled = false;
            tranpuRed = false;
            animator.SetBool("Black", true);
        }

        void RedMode()
        {
            capsuleCol.enabled = true;
            tranpuRed = true;
            animator.SetBool("Black", false);
        }

        //public void AddSwitch(GameObject set)
        //{
        //    inputSwitch.Add(set);
        //}

        bool Direction()
        {
            return transform.position.x <= player.transform.position.x;
        }    
    }
}
