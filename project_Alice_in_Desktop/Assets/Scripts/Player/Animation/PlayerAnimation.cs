using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        // PlayerのAnimation処理

        private Animator _animator;


        private void Start()
        {
            _animator    = GetComponent<Animator>();
        }


        // Animationの変更処理(bool型)
        public void AnimationBoolenChange(int _anim, bool flg)
        {
            _animator.SetBool(_anim, flg);
        }

        // Animationの変更処理(Trigger型)
        public void AnimationTriggerChange(int _anim)
        {
            _animator.SetTrigger(_anim);
        }
    }


}

