using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Connector 
{
    namespace Player 
    {
        // Playerの位置送信インタフェース
        // Sendableな！！

        interface IPlayerPotionSentable 
        {
            Vector3 PlayerPotionSentable();
        }
    }
}