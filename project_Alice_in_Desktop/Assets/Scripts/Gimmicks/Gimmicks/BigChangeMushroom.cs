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
                // Player���傫�����ɏ������Ȃ�L�m�R��H�ׂ��ꍇ���̃T�C�Y�ɖ߂�
                iStatusSentable.PlayerSizeChange_Big();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collision��Player�̏ꍇ�̂�
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                stayFlg = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // collision��Player�̏ꍇ�̂�
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
