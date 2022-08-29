using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        // Player��Animation����

        private Animator _animator;


        private void Start()
        {
            _animator    = GetComponent<Animator>();
        }


        // Animation�̕ύX����(bool�^)
        public void AnimationBoolenChange(int _anim, bool flg)
        {
            _animator.SetBool(_anim, flg);
        }

        // Animation�̕ύX����(Trigger�^)
        public void AnimationTriggerChange(int _anim)
        {
            _animator.SetTrigger(_anim);
        }
    }


}

