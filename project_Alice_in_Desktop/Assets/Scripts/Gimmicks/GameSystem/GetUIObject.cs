using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GetUIObject
    {
        private static GameObject hirakuUI = null;
        private static GameObject switchUI = null;


        private static GameObject keyUI = null;

        public static GameObject HirakuUI
        {
            get => hirakuUI;
            set { if (hirakuUI == null) hirakuUI = value; }
        }

        public static GameObject SwitchUI
        {
            get => switchUI;
            set { if (switchUI == null) switchUI = value; }
        }




        public static GameObject KeyUI
        {
            get => keyUI;
            set { if (keyUI == null) keyUI = value; }
        }
    }
}
