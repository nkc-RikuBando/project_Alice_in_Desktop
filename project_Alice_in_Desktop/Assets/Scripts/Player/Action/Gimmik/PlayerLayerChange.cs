using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLayerChange : MonoBehaviour, IRenderingFlgSettable
    {
        // PlayerのLayerを変更する処理処理

        [SerializeField] private int _insideLayerNum;
        [SerializeField] private int _outsideLayerNum;

        private PlayerStatus _playerStatus;


        private void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
        }


        void IRenderingFlgSettable.SetRenderingFlg(bool val)
        {
            // Windowの中
            if (val)
            {
                gameObject.layer = _insideLayerNum;

                _playerStatus._GroundJudge = true;
                _playerStatus._WallJudge   = true;
                _playerStatus._insideFlg   = true;
            }
            // Windowの外
            else
            {
                gameObject.layer = _outsideLayerNum;

                _playerStatus._GroundJudge = false;
                _playerStatus._WallJudge   = false;
                _playerStatus._insideFlg   = false;
            }
        }
    }

}

