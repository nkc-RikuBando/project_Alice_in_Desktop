using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Inputer
{
    public class KeyboardInputer : MonoBehaviour, IInputReceivable
    {
        // 入力処理用受付インターフェース実装


        // Y軸入力
        float IInputReceivable.MoveH()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        // ジャンプ入力
        bool IInputReceivable.JumpKey()
        {
            return Input.GetButton("Jump");
        }

        // アクション入力(押したとき)
        public bool ActionKey_Down()
        {
            return Input.GetKeyDown(KeyCode.E);
        }

        // アクション入力(押している間)
        public bool ActionKey()
        {
            return Input.GetKey(KeyCode.E);
        }

        //  アクション入力(離したとき)
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
