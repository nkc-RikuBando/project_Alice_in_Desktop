using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtility
{
    public class PlaySE : MonoBehaviour
    {
        // SEÇçƒê∂Ç∑ÇÈèàóù


        [SerializeField] private AudioClip se_1;
        [SerializeField] private AudioClip se_2;
        private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play_1() 
        {
            audioSource.PlayOneShot(se_1);
        }

        public void Play_2()
        {
            audioSource.PlayOneShot(se_2);
        }

    }

}
