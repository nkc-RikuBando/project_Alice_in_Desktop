using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;

namespace Player
{
    public class PlayerPotion : MonoBehaviour, IPlayerPotionSentable
    {
        // Player�̑��M�𑗐M���鏈��

        // �C���^�t�F�[�X������
        Vector3 IPlayerPotionSentable.PlayerPotionSentable()
        {
            return gameObject.transform.position;
        }
    }
}
