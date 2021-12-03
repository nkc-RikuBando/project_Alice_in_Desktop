using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Gimmicks
{
    public class Key : MonoBehaviour
    {
        private GameObject player; // �v���C���[��ۑ�

        private GameObject goal;
        private IGetKey iGetter;

        //private AudioSource se;
        //public AudioClip seClip;

        void Awake()
        {
            player = GameObject.Find("PlayerTest"); // �v���C���[�I�u�W�F�N�g���擾
            goal = GameObject.Find("ClearFlg"); // �S�[���I�u�W�F�N�g���擾
            iGetter = goal.GetComponent<IGetKey>();
            iGetter.AddKey(gameObject);

            //se = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // var tmp = collision.GetComponent<PlayerState>();

            if (collision.gameObject == player)
            {
                //AudioManager.Instance.SeAction("�u�n�E�b�I�v");
                //se.PlayOneShot(seClip);
                iGetter.GetKey(gameObject);
            }

            //if (tmp != null)
            //{
            //    iGetter.GetKey(gameObject);
            //}
            //else 
            //{
            //}
        }
    }
}
