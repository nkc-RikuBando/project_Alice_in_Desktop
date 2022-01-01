using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;

namespace Gimmicks
{
    public class SmallChangeMushroom : MonoBehaviour
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
            if (IsEventKey())
            {
                // Playerが小さい時に大きくなるキノコを食べた場合元のサイズに戻る
                if (iStatusSentable.ScaleMagnification > 1) iStatusSentable.ScaleMagnification = 1;
                else iStatusSentable.ScaleMagnification = 0.5f;
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
