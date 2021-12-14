using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class Key : MonoBehaviour
    {
        private GameObject player; // �v���C���[��ۑ�
        [SerializeField] private string playerName; // �v���C���[�̖��O���擾
        [SerializeField] private string clearFlgName; // �N���A�t���O�̖��O���擾

        private GameObject goal;
        private IGetKey iGetter;

        //private AudioSource se;
        //public AudioClip seClip;
        //[SerializeField] private string seName;

        void Awake()
        {
            player = GameObject.Find(playerName); // �v���C���[�I�u�W�F�N�g���擾
            goal = GameObject.Find(clearFlgName); // �S�[���I�u�W�F�N�g���擾
            iGetter = goal.GetComponent<IGetKey>();
            iGetter.AddKey(gameObject);

            //se = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                //AudioManager.Instance.SeAction(seName);
                //se.PlayOneShot(seClip);
                iGetter.GetKey(gameObject);
            }
        }
    }
}
