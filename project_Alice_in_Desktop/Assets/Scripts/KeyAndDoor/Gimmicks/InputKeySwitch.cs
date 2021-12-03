using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour
    {   
        [SerializeField] private GameObject gimmick;
        private bool addSwitch = false;
        private GameObject player;
        private ITestKey _ITestKey;

        private bool stayFlg;

        void Start()
        {
            player = GameObject.Find("PlayerTest"); // �v���C���[�I�u�W�F�N�g���擾
            _ITestKey = GetComponent<ITestKey>();
            stayFlg = false;
        }

        private void Update()
        {
            if(StayInput())
            {
                Debug.Log("W");
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
