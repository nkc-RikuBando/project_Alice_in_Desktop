using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        // PlayerのAnimation処理

        private PlayerState _playerState;

        private void Start()
        {
            _playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
        }


        // Animationの変更処理
        private void AnimationChange()
        {
            switch (_playerState._StateEnum)
            {
                case PlayerState.PlayerStateEnum.STAY:
                    break;
                case PlayerState.PlayerStateEnum.WALK:
                    break;
                case PlayerState.PlayerStateEnum.JUMP_UP:
                    break;
                case PlayerState.PlayerStateEnum.JUMP_PREVIOUS:
                    break;
                case PlayerState.PlayerStateEnum.JUMP_DOWN:
                    break;
                case PlayerState.PlayerStateEnum.LANDING:
                    break;
                case PlayerState.PlayerStateEnum.WALLSTICK:
                    break;
                default:
                    break;
            }

        }


        // Animationを初期化
        private void AniamtionReset()
        {

        }

    }

}
