using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        // Player‚ÌAnimationˆ—

        private Animator _animator;


        private void Start()
        {
            _animator    = GetComponent<Animator>();
        }


        // Animation‚Ì•ÏXˆ—(boolŒ^)
        public void AnimationBoolenChange(int _anim, bool flg)
        {
            _animator.SetBool(_anim, flg);
        }

        // Animation‚Ì•ÏXˆ—(TriggerŒ^)
        public void AnimationTriggerChange(int _anim)
        {
            _animator.SetTrigger(_anim);
        }
    }


}

