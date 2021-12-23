using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameSystem
{
    public static class GetGameObject 
    {
        private static GameObject playerObj = null;

        public static GameObject playerObject
        {
            get => playerObj;
            set
            {
                if (playerObj == null) playerObj = value;
                //else Debug.Log("Ç∑Ç≈Ç…PlayerÇÕë∂ç›ÇµÇ‹Ç∑");
            }
        }
    }
}