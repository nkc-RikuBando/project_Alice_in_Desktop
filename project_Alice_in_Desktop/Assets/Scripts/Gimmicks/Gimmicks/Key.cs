using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private GameObject player; // �v���C���[��ۑ�
        [SerializeField] private GameObject goal;   // �S�[�����擾
        private IGetKey iGetter;

        //private AudioSource se;
        //public AudioClip seClip;
        //[SerializeField] private string seName;

        void Awake()
        {
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
