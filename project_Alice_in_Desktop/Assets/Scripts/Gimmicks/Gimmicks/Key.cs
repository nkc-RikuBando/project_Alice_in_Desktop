using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class Key : MonoBehaviour
    {
        private GameObject player;
        private GameObject goal;   // ÉSÅ[ÉãÇéÊìæ
        private IGetKey iGetter;
        private Animator animator;

        //private AudioSource se;
        //public AudioClip seClip;
        //[SerializeField] private string seName;

        void Awake()
        {
            goal = GameObject.Find("Door_Complete");
            iGetter = goal.GetComponent<IGetKey>();
            iGetter.AddKey(gameObject);
        }

        void Start()
        {
            player = GetGameObject.playerObject;
            
            animator = GetComponent<Animator>();
            //se = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                //AudioManager.Instance.SeAction(seName);
                //se.PlayOneShot(seClip);
                animator.SetTrigger("Get");
                iGetter.GetKey(gameObject);
            }
        }
    }
}
