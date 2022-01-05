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
        private IPlayerAction iPlayeraction;
        private IPlayerStatusSentable iStatusSentable;
        private bool stayFlg = false;

        void Start()
        {
            player = GetGameObject.playerObject;
            iPlayeraction = player.GetComponent<IPlayerAction>();
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
        }

        void Update()
        {
            if(IsEventKey())
            {
                // Playerが大きい時に小さくなるキノコを食べた場合元のサイズに戻る
                iStatusSentable.PlayerSizeChange_Big();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collisionがPlayerの場合のみ
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                stayFlg = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // collisionがPlayerの場合のみ
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                stayFlg = false;
            }
        }

        bool IsEventKey()
        {
            return iPlayeraction.ActionKey_Down() && stayFlg == true;
        }
    }
}
