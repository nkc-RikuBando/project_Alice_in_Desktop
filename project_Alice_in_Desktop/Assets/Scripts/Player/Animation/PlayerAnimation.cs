using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        // PlayerÇÃAnimationèàóù

        private PlayerState _playerState;
        private Animator    _animator;

        private readonly int DashFlg = Animator.StringToHash("Dash");
        private readonly int JumpFlg = Animator.StringToHash("Jump");
        private readonly int FallFlg = Animator.StringToHash("Fall");
        private readonly int StickFlg = Animator.StringToHash("Stick");

        private void Start()
        {
            _playerState = GetComponent<PlayerState>();
            _animator    = GetComponent<Animator>();
        }

        private void Update()
        {
            AnimationChange();
        }


        // AnimationÇÃïœçXèàóù
        private void AnimationChange()
        {
            Debug.Log(_playerState._StateEnum);

            switch (_playerState._StateEnum)
            {
                case PlayerState.PlayerStateEnum.STAY:
                    _animator.SetBool(DashFlg, false);
                    break;
                case PlayerState.PlayerStateEnum.DASH:
                    _animator.SetBool(DashFlg,true);
                    break;
                case PlayerState.PlayerStateEnum.JUMP_UP:
                    //_animator.SetTrigger(JumpFlg);
                    break;
                case PlayerState.PlayerStateEnum.JUMP_PREVIOUS:
                    //_animator.SetTrigger(JumpFlg);
                    break;
                case PlayerState.PlayerStateEnum.JUMP_DOWN:
                    //_animator.SetBool(FallFlg, true);
                    break;
                case PlayerState.PlayerStateEnum.LANDING:
                    //_animator.SetBool(FallFlg, false);
                    break;
                case PlayerState.PlayerStateEnum.WALLSTICK:
                    //_animator.SetBool(StickFlg,true);
                    break;
                default:
                    break;
            }

        }


        // AnimationÇèâä˙âª
        private void AniamtionReset()
        {
            _animator.SetBool(DashFlg, true);
            _animator.SetTrigger(JumpFlg);
            _animator.SetBool(FallFlg, true);
            _animator.SetBool(StickFlg, true);
        }

    }

}
