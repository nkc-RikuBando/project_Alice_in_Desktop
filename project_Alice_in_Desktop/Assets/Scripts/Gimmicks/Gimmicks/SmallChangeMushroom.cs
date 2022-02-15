using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;
using Player;

namespace Gimmicks
{
    public class SmallChangeMushroom : MonoBehaviour
    {
        private GameObject player;
        private IPlayerStatusSentable iStatusSentable;
        private MushRoomEvent roomEvent;

        private int sizeChangeCount;

        void Start()
        {
            player = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();

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
                    // Player�̃T�C�Y��ύX
                    // Animation���Đ�����
                    iStatusSentable.PlayerSizeChange(0.5f);
                    roomEvent.PlayerSizeChange();

                    sizeChangeCount = 0;
                }
            }
        }

    }
}
