using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour
    {
        [SerializeField] private GameObject player; // プレイヤーを取得
        private ITestKey _ITestKey; // 入力インターフェースを保存
        [SerializeField] private GameObject gimmick;
        private bool addSwitch; // 
        private bool stayFlg;   // 

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>(); // 入力インターフェースを取得
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
            if (collision.gameObject == player) stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player) stayFlg = false;
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、Qキーを押す
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventKey();
        }
    }
}
