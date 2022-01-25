using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;

namespace Player
{
    public class PlayerPotion : MonoBehaviour, IPlayerPotionSentable
    {
        Vector3 IPlayerPotionSentable.PlayerPotionSentable()
        {
            return gameObject.transform.position;
        }
    }
}
