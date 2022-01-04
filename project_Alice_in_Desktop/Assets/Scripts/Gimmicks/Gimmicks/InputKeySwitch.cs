using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour
    {
        private GameObject player; // プレイヤーを取得
        private IPlayerAction _IActionKey; // 入力インターフェースを保存
        private GameObject gimmick;
        private GameObject switchUI;
        private bool addSwitch; // 
        private bool stayFlg;   // 

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // 入力インターフェースを取得
            gimmick = GetGameObject.GimmickObj;
            switchUI = GetUIObject.SwitchUI;
            switchUI.SetActive(false);
            addSwitch = false;
            stayFlg = false;
        }

        private void Update()
        {
            if(StayInput())
            {
                IHitSwitch hitGimmick = gimmick.GetComponent<IHitSwitch>();
                if (hitGimmick != null)
                {
                    addSwitch = addSwitch ? false : true;
                    hitGimmick.Switch(addSwitch);
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
            {
                stayFlg = true;
                switchUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false;
                switchUI.SetActive(false);
            }
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、Qキーを押す
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _IActionKey.ActionKey_Down();
        }
    }
}
