using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameSystem
{
    public static class GetGameObject 
    {
        private static GameObject playerObj = null; // �v���C���[��ۑ�
        private static GameObject clearDoor = null; // �v���C���[��ۑ�

        // �v���C���[���擾
        public static GameObject playerObject
        {
            get => playerObj;
            set
            {
                if (playerObj == null) playerObj = value;
                //else Debug.Log("���ł�Player�͑��݂��܂�");
            }
        }
    }
}