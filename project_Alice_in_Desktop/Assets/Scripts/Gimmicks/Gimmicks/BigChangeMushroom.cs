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
        private GameObject player;
        private IPlayerStatusSentable iStatusSentable;
        private MushRoomEvent roomEvent;
        private int sizeChangeCount = 0;

        void Start()
        {
            player = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
            // Œã‚ÅÁ‚·
            roomEvent = player.GetComponent<MushRoomEvent>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collision‚ªPlayer‚Ìê‡‚Ì‚İ
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                sizeChangeCount++;

                if (sizeChangeCount % 2 == 0)
                {
                    // Animation‚ğÄ¶‚·‚é
                    iStatusSentable.PlayerSizeChange(1.5f);
                    roomEvent.PlayerSizeChange();
                    sizeChangeCount = 0;
                }

            }
        }
    }
}
