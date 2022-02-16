using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLayerChange : MonoBehaviour, IRenderingFlgSettable
    {
        // Player‚ÌLayer‚ğ•ÏX‚·‚éˆ—ˆ—

        [SerializeField] private int _insideLayerNum;
        [SerializeField] private int _outsideLayerNum;


        private PlayerStatus _playerStatus;
        private GameObject _childObj;


        private void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
            _childObj = transform.GetChild(0).gameObject;
        }

        void IRenderingFlgSettable.SetRenderingFlg(bool val)
        {
            // Window‚Ì’†
            if (val)
            {
                gameObject.layer = _insideLayerNum;

                _playerStatus._GroundJudge = true;
                _playerStatus._WallJudge   = true;
                _playerStatus._InsideFlg   = true;
                _playerStatus._PushJudge   = true;
            }
            // Window‚ÌŠO
            else
            {
                gameObject.layer = _outsideLayerNum;

                _playerStatus._GroundJudge = false;
                _playerStatus._WallJudge   = false;
                _playerStatus._InsideFlg   = false;
                _playerStatus._PushJudge   = false;

                _playerStatus._DeadColFlg = false;
            }
        }
    }

}

