using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        // Player‚ÌAnimationˆ—

        private PlayerState _playerState;

        private void Start()
        {
            _playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
        }


        // Animation‚Ì•ÏXˆ—
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


        // Animation‚ğ‰Šú‰»
        private void AniamtionReset()
        {

        }

    }

}
