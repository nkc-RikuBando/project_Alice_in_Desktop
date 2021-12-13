using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour
    {
        [SerializeField] private GameObject player; // プレイヤーを取得
        private ITestKey _ITestKey;
        [SerializeField] private GameObject gimmick;
        private bool addSwitch = false;

        private bool stayFlg;

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>();
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
            if (collision.gameObject == player) stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
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
