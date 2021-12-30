using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;
using Player;

namespace Gimmicks
{
    public class SizeChangeMushroom : MonoBehaviour
    {
        private GameObject player;
        [SerializeField] private GameObject playerFront;
        private IPlayerAction iPlayeraction;
        private Vector3 playerSize;
        private bool stayFlg;

        //[Header("è¨Ç≥Ç≠Ç»ÇÈ")]
        //[SerializeField] private bool scaleFlg = false;

        void Start()
        {
            player = GetGameObject.playerObject;
            player.GetComponent<PlayerMove>();
            iPlayeraction = player.GetComponent<IPlayerAction>();
            //playerSize = player.transform.localScale;
            //playerSize = playerFront.transform.localScale;
            //playerSize = new Vector3(1, 1, 1);
            stayFlg = false;
        }

        void Update()
        {
            if(IsEventKey())
            {
                Debug.Log("asdf");
                //playerSize = new Vector3(3 * (-1), 3, 1);
                
            }
            //player.transform.localScale=playerSize;
            //playerFront.transform.localScale = playerSize;
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
