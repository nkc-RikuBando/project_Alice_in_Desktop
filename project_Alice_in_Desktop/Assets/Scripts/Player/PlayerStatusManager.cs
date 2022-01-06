using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;

namespace Player
{
    public class PlayerStatusManager : MonoBehaviour, IPlayerStatusSentable
    {
        private PlayerStatus _playerStatus;

        private void Awake()
        {
            _playerStatus = GetComponent<PlayerStatus>();
        }


        // ���͎�t�Ǘ����\�b�h
        public void PlayerIsInput(bool flg)
        {
            _playerStatus._InputFlgX = flg;
            _playerStatus._InputFlgY = flg;
            _playerStatus._InputFlgAction = flg;
        }

        // Player�T�C�Y�ύX���\�b�h(�g��)
        void IPlayerStatusSentable.PlayerSizeChange_Small()
        {
            bool _isSmallScale   = transform.localScale.x >= 1 && transform.localScale.y >= 1;
            bool _isDefaultScale = transform.localScale.x > 1 && transform.localScale.y > 1;

            if (_isSmallScale)
            {
                transform.localScale = new Vector3(transform.localScale.x * _playerStatus.SmallSizeMag,
                                                   transform.localScale.y * _playerStatus.SmallSizeMag,
                                                   1f);

                if (_isDefaultScale)
                {
                    transform.localScale = new Vector3(transform.localScale.x * 1f,
                                                       transform.localScale.y * 1f,
                                                       1f);
                }
            }
        }

        // Player�T�C�Y�ύX���\�b�h(�k��)
        void IPlayerStatusSentable.PlayerSizeChange_Big()
        {
            bool _isBigScale     = transform.localScale.x <= 1 && transform.localScale.y <= 1;
            bool _isDefaultScale = transform.localScale.x < 1 && transform.localScale.y < 1;

            if (_isBigScale)
            {
                transform.localScale = new Vector3(transform.localScale.x * _playerStatus.BigSizeMag,
                                                   transform.localScale.y * _playerStatus.BigSizeMag,
                                                   1f);

                if (_isDefaultScale)
                {
                    transform.localScale = new Vector3(transform.localScale.x * 1f,
                                                       transform.localScale.y * 1f,
                                                       1f);
                }

            }
        }
    }

}
