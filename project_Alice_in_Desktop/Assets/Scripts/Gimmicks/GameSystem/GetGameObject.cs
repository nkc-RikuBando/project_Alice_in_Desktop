using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameSystem
{
    public static class GetGameObject 
    {
        private static GameObject playerObj = null; // プレイヤーを保存
        private static GameObject clearDoor = null; // プレイヤーを保存

        // プレイヤーを取得
        public static GameObject playerObject
        {
            get => playerObj;
            set
            {
                if (playerObj == null) playerObj = value;
                //else Debug.Log("すでにPlayerは存在します");
            }
        }
    }
}