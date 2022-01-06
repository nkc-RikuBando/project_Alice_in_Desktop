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

        [SerializeField] private GameObject[] inputSwitch;
        private ISetToranpuSoldier iSetSoldier;
        private ISetToranpuSoldier iSetSoldier1;
        //private ISetToranpuSoldier iSetSoldier1;

        private bool switchFlg;
        private bool tranpuRed = true;
        [Header("�g�����v����")]
        [SerializeField] private bool blackOn;

        void Awake()
        {
            //for (int i = 0; i < inputSwitch.Length; i++)
            //{
            //    inputSwitch[i].GetComponent<IHitSwitch>();
            //    iSetSoldier[i].AddSoldier(gameObject);
            //}


            iSetSoldier = inputSwitch[0].GetComponent<ISetToranpuSoldier>();
            iSetSoldier1 = inputSwitch[1].GetComponent<ISetToranpuSoldier>();
            iSetSoldier.AddSoldier(gameObject);
            iSetSoldier1.AddSoldier(gameObject);
        }

        void Start()
        {
            player = GetGameObject.playerObject; // �v���C���[���擾
            animator = GetComponent<Animator>(); // �A�j���[�^�[���擾
            boxCol = GetComponent<BoxCollider2D>();

            if (blackOn == true)
            {
                animator.SetBool("Black", true);
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
            boxCol.enabled = false;
            tranpuRed = false;
            animator.SetBool("Black", true);
        }

        void RedMode()
        {
            boxCol.enabled = true;
            tranpuRed = true;
            animator.SetBool("Black", false);
        }

        bool Direction()
        {
            return transform.position.x <= player.transform.position.x;
        }
    }
}
