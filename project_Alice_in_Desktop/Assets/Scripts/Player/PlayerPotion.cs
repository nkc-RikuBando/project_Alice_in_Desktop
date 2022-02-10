using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;

namespace Player
{
    public class PlayerPotion : MonoBehaviour, IPlayerPotionSentable
    {
        // Playerの送信を送信する処理

        // インタフェースを実装
        Vector3 IPlayerPotionSentable.PlayerPotionSentable()
        {
            return gameObject.transform.position;
        }
    }
}
