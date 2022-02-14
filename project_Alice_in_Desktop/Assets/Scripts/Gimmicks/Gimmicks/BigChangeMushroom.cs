using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;

namespace Gimmicks
{
    public class BigChangeMushroom : MonoBehaviour
    {
        private GameObject player;
        private IPlayerStatusSentable iStatusSentable;
        private int sizeChangeCount = 0;

        void Start()
        {
            player = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
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
                    sizeChangeCount = 0;
                }

            }
        }
    }
}
