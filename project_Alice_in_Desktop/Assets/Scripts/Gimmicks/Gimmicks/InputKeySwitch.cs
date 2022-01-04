using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour
    {
        private GameObject player; // �v���C���[���擾
        private IPlayerAction _IActionKey; // ���̓C���^�[�t�F�[�X��ۑ�
        private GameObject gimmick;
        private GameObject switchUI;
        private bool addSwitch; // 
        private bool stayFlg;   // 

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // ���̓C���^�[�t�F�[�X���擾
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
            // �v���C���[�������ė�����
            if (collision.gameObject == player)
            {
                stayFlg = true;
                switchUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false;
                switchUI.SetActive(false);
            }
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���AQ�L�[������
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _IActionKey.ActionKey_Down();
        }
    }
}
