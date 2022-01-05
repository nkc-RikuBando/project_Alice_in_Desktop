using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Inputer
{
    public class KeyboardInputer : MonoBehaviour, IInputReceivable
    {
        // ���͏����p��t�C���^�[�t�F�[�X����


        // Y������
        float IInputReceivable.MoveH()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        // �W�����v����(W)
        bool IInputReceivable.JumpKey_W()
        {
            return Input.GetKeyDown(KeyCode.W);
        }

        // �W�����v����(Space)
        bool IInputReceivable.JumpKey_Space()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }


        // �A�N�V��������(�������Ƃ�)
        public bool ActionKey_Down()
        {
            return Input.GetKeyDown(KeyCode.E);
        }

        // �A�N�V��������(�����Ă����)
        public bool ActionKey()
        {
            return Input.GetKey(KeyCode.E);
        }

        //  �A�N�V��������(�������Ƃ�)
        public bool ActionKeyUp()
        {
            return Input.GetKeyUp(KeyCode.E);
        }
    }
}
