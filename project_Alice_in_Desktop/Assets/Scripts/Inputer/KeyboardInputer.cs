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

        // �W�����v����
        bool IInputReceivable.JumpKey()
        {
            return Input.GetButton("Jump");
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

        bool IInputReceivable.WallJumpKey_A()
        {
            return Input.GetKeyDown(KeyCode.A);
        }

        bool IInputReceivable.WallJumpKey_D()
        {
            return Input.GetKeyDown(KeyCode.D);
        }

        bool IInputReceivable.MoveKey_A()
        {
            return Input.GetKey(KeyCode.A);
        }

        bool IInputReceivable.MoveKey_D()
        {
            return Input.GetKey(KeyCode.D);
        }
    }
}
