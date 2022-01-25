using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GetUIObject
    {
        private static GameObject keyUI = null;

        public static GameObject KeyUI
        {
            get => keyUI;
            set { if (keyUI == null) keyUI = value; }
        }
    }
}
