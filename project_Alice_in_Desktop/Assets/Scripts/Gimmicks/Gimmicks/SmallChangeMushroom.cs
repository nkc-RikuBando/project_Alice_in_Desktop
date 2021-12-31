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
        private Vector3 playerSize;
        private bool stayFlg = false;

        void Start()
        {
            player = GetGameObject.playerObject;
            iPlayeraction = player.GetComponent<IPlayerAction>();
            playerSize = player.transform.localScale;
        }

        void Update()
        {
            if (IsEventKey())
            {
                playerSize = new Vector3(0.5f, 0.5f, 1);
            }
            player.transform.localScale = playerSize;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            stayFlg = false;
        }

        bool IsEventKey()
        {
            return iPlayeraction.ActionKey_Down() && stayFlg == true;
        }
    }
}
