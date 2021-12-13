using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour
    {
        [SerializeField] private GameObject player; // �v���C���[���擾
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
        /// �v���C���[���G��Ă���A���AQ�L�[������
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventKey();
        }
    }
}
