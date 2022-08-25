using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtility
{
    public class FootSteps_SE : MonoBehaviour
    {
        // 足音SEを再生する処理


        [SerializeField] private AudioClip se_Right;
        [SerializeField] private AudioClip se_Left;
        private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void FootSteps_Right() 
        {
            audioSource.PlayOneShot(se_Right);
        }

        public void FootSteps_Left()
        {
            audioSource.PlayOneShot(se_Left);
        }

    }

}
