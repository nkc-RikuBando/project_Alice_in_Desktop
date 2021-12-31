using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameSystem
{
    public static class GetGameObject 
    {
        private static GameObject playerObj = null;
        private static GameObject frame = null;
        private static GameObject windowCol = null;
        private static GameObject key = null;
        private static GameObject gimmick = null;

        public static GameObject playerObject
        {
            get => playerObj;
            set
            {
                if (playerObj == null) playerObj = value;
                //else Debug.Log("‚·‚Å‚ÉPlayer‚Í‘¶Ý‚µ‚Ü‚·");
            }
        }

        public static GameObject FrameObject
        {
            get => frame;
            set
            {
                if (frame == null) frame = value;
            }
        }

        public static GameObject WindowColObject
        {
            get => windowCol;
            set
            {
                if (windowCol == null) windowCol = value;
            }
        }

        public static GameObject KeyObj
        {
            get => key;
            set
            {
                if (key == null) key = value;
            }
        }

        public static GameObject GimmickObj
        {
            get => gimmick;
            set
            {
                if (gimmick == null) gimmick = value;
            }
        }
    }
}