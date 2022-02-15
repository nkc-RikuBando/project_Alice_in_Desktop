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
        [SerializeField] private AudioClip se;

        private GameObject player;
        private IPlayerStatusSentable iStatusSentable;
        private AudioSource audioSource;
        private MushRoomEvent roomEvent;
        private int sizeChangeCount = 0;

        void Start()
        {
            player = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
            audioSource = GetComponent<AudioSource>();
            // ��ŏ���
            roomEvent = player.GetComponent<MushRoomEvent>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collision��Player�̏ꍇ�̂�
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                sizeChangeCount++;

                if (sizeChangeCount % 2 == 0)
                {
                    // Animation���Đ�����
                    iStatusSentable.PlayerSizeChange(1.5f);
                    audioSource.PlayOneShot(se);
                    roomEvent.PlayerSizeChange();
                    sizeChangeCount = 0;
                }

            }
        }
    }
}
