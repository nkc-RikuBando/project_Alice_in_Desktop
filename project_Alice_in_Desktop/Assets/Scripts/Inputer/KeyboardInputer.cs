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

        // ジャンプ入力(W)
        bool IInputReceivable.JumpKey_W()
        {
            return Input.GetKeyDown(KeyCode.W);
        }

        // ジャンプ入力(Space)
        bool IInputReceivable.JumpKey_Space()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

    }
}
