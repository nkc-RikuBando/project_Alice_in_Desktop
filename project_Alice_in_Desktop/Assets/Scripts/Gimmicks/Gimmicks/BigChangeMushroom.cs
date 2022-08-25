using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;
using Player;

namespace Gimmicks
{
    public class BigChangeMushroom : MonoBehaviour
    {
    �@�@// �傫���Ȃ�L�m�R�̏���

        [SerializeField] private AudioClip se;

        private GameObject �@�@�@�@�@ player;
        private IPlayerStatusSentable iStatusSentable;
        private AudioSource           audioSource;

        private int sizeChangeCount = 0;

        void Start()
        {
            player �@�@�@�@ = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
            audioSource�@   = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (iStatusSentable.GetIsWindowTouch()) return;

            // collision��Player�̏ꍇ�̂�
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                // Player�̃R���C�_�[������Ă�
                sizeChangeCount++;

                if (sizeChangeCount % 2 == 0)
                {
                    // Animation���Đ�����
                    iStatusSentable.PlayerSizeChange(1.5f);

                    if (player.transform.localScale.y >= 1.5f) return;
                    iStatusSentable.PlayerBiggerAnimation();
                    audioSource.PlayOneShot(se);

                    sizeChangeCount = 0;
                }
            }
        }
    }
}
